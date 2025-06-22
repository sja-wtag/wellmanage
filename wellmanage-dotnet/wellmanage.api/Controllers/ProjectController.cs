using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wellmanage.application.Interfaces;
using wellmanage.application.Services;
using wellmanage.domain.Entity;

namespace wellmanage_dotnet.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IProjectTaskService _projectTaskService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService,IProjectTaskService projectTaskService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _projectTaskService = projectTaskService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>List of all projects.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                if (projects == null || !projects.Any())
                {
                    return NotFound("No projects found.");
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all projects.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the projects.");
            }
        }


        /// <summary>
        /// Gets a single project by its ID.
        /// </summary>
        /// <param name="projectId">The ID of the project.</param>
        /// <returns>Project details.</returns>
        [Authorize]
        [HttpGet("{projectId:long}")]
        public async Task<IActionResult> GetProjectById(long projectId)
        {
            try
            {
                var response = await _projectService.GetProjectByIdAsync(projectId);
                if (response == null)
                {
                    return NotFound($"Project with ID {projectId} not found.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching project with ID {ProjectId}", projectId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the project.");
            }
        }

        [Authorize]
        [HttpGet("{projectId}/tasks")]
        public async Task<ActionResult<ProjectTask>> GetTasksForEmployee(long projectId)
        {
            if (projectId <= 0)
            {
                return BadRequest("Invalid project ID.");
            }

            var tasks = await _projectTaskService.GetAllTasksInAProjectAsync(projectId);

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound("No tasks found for the specified user.");
            }

            return Ok(tasks);
        }
    }
}
