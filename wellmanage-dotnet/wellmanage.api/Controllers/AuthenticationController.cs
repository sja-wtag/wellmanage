using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using wellmanage.api.Enums;
using wellmanage.api.Models;
using wellmanage.application.Interfaces;
using wellmanage.application.Models;
using wellmanage.domain.Entity;
using wellmanage.shared.Enums;


namespace wellmanage.api.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{v:apiVersion}/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;

    public AuthenticationController(UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        IConfiguration configuration,
        ILogger<AuthenticationController> logger,
        ITokenService tokenService,
        IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _logger = logger;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    [IgnoreAntiforgeryToken]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegistrationRequest request)
    {
        User user = null;
        try
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);

            if (userExists != null)
            {
                var response = new ApiResponse
                {
                    Status = "Error",
                    Message = "User already exists!",
                    NotifyUser = true,
                    HasError = true,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            user = new User()
            {
                FullName = request.FullName,
                UserName = request.Email.Split("@")[0].ToUpper(),
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Status = StatusEnum.Active
            };


            var result = await _userManager.CreateAsync(user, request.Password);


            if (!result.Succeeded)
            {
                var response = new ApiResponse
                {
                    Status = "Error",
                    Message = "User creation failed! Please check error details and try again.",
                    HasError = true,
                    ErrorList = result.Errors.Select(error => error.Description).ToList(),
                    NotifyUser = true
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                var response = new ApiResponse();

                if (request.VerificationType == VerificationTypeEnum.ConfirmationLink)
                {
                    response = await SendConfirmationEmailToUser(user);
                }
                else if (request.VerificationType == VerificationTypeEnum.OTPCode)
                {
                    response = await SendOTPToUser(user);
                }

                return StatusCode(response.StatusCode, response);
            }
        }
        catch (Exception ex)
        {
            if (user != null)
                await _userManager.DeleteAsync(user);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResponse { Status = "Failure!", Message = ex.Message });
            ;
        }
    }

    [IgnoreAntiforgeryToken]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ApiResponse { Status = "Error", Message = "User Not Found!", NotifyUser = true });

            bool emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (!emailConfirmed)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ApiResponse { Status = "Error", Message = "Please confirm your email!", NotifyUser = true });
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result)
            {
                var authClaims = _tokenService.PrepareAuthClaimFromUser(user);
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var accessToken = _tokenService.GetAccessToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();

                AuthenticatedUser authenticatedUser = new()
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Id = user.Id,
                    AuthenticationToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshToken,
                    RoleNames = roles.ToArray()
                };
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(8);
                await _userManager.UpdateAsync(user);

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<AuthenticatedUser>
                {
                    Status = "Success",
                    Message = "User login Successfully!",
                    ResponseData = authenticatedUser,
                    NotifyUser = true
                });
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new ApiResponse { Status = "Error", Message = "User login Failed!", NotifyUser = true });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                new ApiResponse { Status = "Error", Message = ex.ToString(), NotifyUser = true });
        }
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest tokenRequest)
    {
        if (tokenRequest is null)
            return BadRequest("Invalid client request");

        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);

        if (principal?.Identity?.Name is null)
            return Unauthorized(AuthTokenResponseTypeEnum.InvalidAccessToken);

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (user is null || user.RefreshToken != tokenRequest.RefreshToken ||
            user.RefreshTokenExpiryTime < DateTime.UtcNow)
            return Unauthorized(AuthTokenResponseTypeEnum.InvalidRefreshToken);

        var authClaims = _tokenService.PrepareAuthClaimFromUser(user);

        var newAccessToken = _tokenService.GetAccessToken(authClaims);
        AuthenticatedUser authenticatedUser = new()
        {
            FullName = user.FullName,
            UserName = user.UserName,
            Id = user.Id,
            AuthenticationToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = tokenRequest.RefreshToken,
        };
        return StatusCode(StatusCodes.Status200OK, new ApiResponse<AuthenticatedUser>
        {
            Status = "Success",
            Message = "Token Refresh Success!",
            ResponseData = authenticatedUser,
            NotifyUser = false
        });
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                var response = new ApiResponse { Status = "Success", Message = "Email Verified Successfully" };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                var response = new ApiResponse { Status = "Error", Message = "Some Error Occured" };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        else
        {
            var response = new ApiResponse { Status = "Error", Message = "User Does not exist!" };
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }
    }

    [HttpPost("confirm-otp")]
    public async Task<IActionResult> ConfirmOTP(OTPVerificationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider,
            "EmailConfirmation", request.OTPCode);
        if (isValid)
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            var response = new ApiResponse
            {
                Status = "Success",
                StatusCode = StatusCodes.Status200OK,
                Message = $"OTP Verified Successfully",
                NotifyUser = true
            };
            return StatusCode(StatusCodes.Status200OK, response);
        }
        else
        {
            var response = new ApiResponse
            {
                Status = "Error",
                Message = "OTP Verification Failed!",
                NotifyUser = true
            };
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }
    }

    private async Task<ApiResponse> SendOTPToUser(User user)
    {
        try
        {
            var token = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultPhoneProvider,
                "EmailConfirmation");
            var message = new Message(new string[] { user.Email! }, "OTP Confirmation", token);
            // string otpTemplatePath = Path.Combine(TemplateConstant.OTPTemplateURL, TemplateConstant.OTPTemplateFile);
            //
            // if (Directory.Exists(TemplateConstant.OTPTemplateURL))
            // {
            //     string emailTemplate = System.IO.File.ReadAllText(otpTemplatePath);
            //     string[] emailBody = emailTemplate.Split("email-body");
            //     string[] otpCode = token.Select(c => c.ToString()).ToArray();
            //     string[] args = { user.UserName };
            //     args = args.Concat(otpCode).ToArray();
            //     emailBody[1] = string.Format(emailBody[1], args);
            //     string htmlBody = string.Join("email-body", emailBody);
            //     message.ContentType = MimeKit.Text.TextFormat.Html;
            //     message.Content = htmlBody;
            // }

            await _emailService.SendEmail(message);
            return new ApiResponse
            {
                Status = "Success",
                StatusCode = StatusCodes.Status200OK,
                Message = $"An OTP Code has been  sent to {user.Email}."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse
            {
                Status = "Failure!",
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = $"Unable to send email."
            };
        }
    }

    private async Task<ApiResponse> SendConfirmationEmailToUser(User user)
    {
        try
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new { token, email = user.Email },
                Request.Scheme);
            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
            await _emailService.SendEmail(message);
            return new ApiResponse
            {
                Status = "Success",
                StatusCode = StatusCodes.Status200OK,
                Message =
                    $"User created & Confirmation Email Sent to {user.Email} <strong class='text-success'>Successfully.</strong>"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                Status = "Failure!", StatusCode = StatusCodes.Status500InternalServerError,
                Message = $"Unable to send email.", NotifyUser = true
            };
        }
    }
}