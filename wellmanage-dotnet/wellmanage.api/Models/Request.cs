using System.ComponentModel.DataAnnotations;
using wellmanage.api.Enums;

namespace wellmanage.api.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}

public class RegistrationRequest
{
    [Required(ErrorMessage = "Full Name is required")]
    public string FullName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    public VerificationTypeEnum? VerificationType { get; set; } = VerificationTypeEnum.OTPCode;

    public string? SuperUserSigningKey { get; set; }
}

public class OTPVerificationRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "OTP is required")]
    public string OTPCode { get; set; }
}

public class RefreshTokenRequest
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}