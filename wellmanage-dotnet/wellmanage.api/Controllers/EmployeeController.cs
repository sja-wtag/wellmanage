using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wellmanage.application.Interfaces;
using wellmanage.application.Services;
using wellmanage.shared.Models;

namespace wellmanage_dotnet.Controllers
{

    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeSaveRequest request)
        {
            try
            {
                if (request.UserId == 0)
                {
                    return BadRequest();
                }
                await _employeeService.AddEmployee(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
