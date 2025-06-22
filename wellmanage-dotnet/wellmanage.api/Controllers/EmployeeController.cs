using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wellmanage.application.Interfaces;
using wellmanage.application.Services;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage_dotnet.Controllers
{

    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;
        private readonly IProjectTaskService _projectTaskService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, IProjectService projectService, IProjectTaskService projectTaskService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _projectService = projectService;
            _projectTaskService = projectTaskService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesWithUserInformation()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithUserInformation();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving employees with user information.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
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

        [Authorize]
        [HttpGet("{employeeId}/projects")]
        public async Task<IActionResult> GetProjectsByEmployeeId(long employeeId)
        {
            try
            {
                var response = await _projectService.GetProjectsByEmployeeIdAsync(employeeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching projects for employee ID {EmployeeId}", employeeId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving projects.");
            }
        }

        [Authorize]
        [HttpGet("{employeeId}/tasks")]
        public async Task<ActionResult<ProjectTask>> GetTasksForEmployee(long employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var tasks = await _projectTaskService.GetTasksForEmployeeAsync(employeeId);

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound("No tasks found for the specified user.");
            }

            return Ok(tasks);
        }
    }
}
