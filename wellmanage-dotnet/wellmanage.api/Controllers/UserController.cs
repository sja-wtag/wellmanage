using System.Security.Claims;
using Asp.Versioning;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using wellmanage.application.Interfaces;
using wellmanage.shared.Models;

namespace wellmanage_dotnet.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/user")]


    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [Authorize]
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckInUser()
        {
            try
            {
                var userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await _userService.MarkUserCheckIn(userId);
                if (response.HasError)
                {
                    return BadRequest(response);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOutUser()
        {
            try
            {
                var userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await _userService.MarkUserCheckOut(userId);
                if (response.HasError)
                {
                    return BadRequest(response);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [Authorize]
        [HttpGet("attendence-status")]
        public async Task<IActionResult> GetAttendenceStatus()
        {
            try
            {
                var userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await _userService.GetAttendenceStatus(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

