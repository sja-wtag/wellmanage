using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wellmanage.application.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage_dotnet.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/projects/{projectId}/tasks")]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTasksController(IProjectTaskService service)
        {
            _projectTaskService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectTask>>> GetAll()
        {
            return Ok(await _projectTaskService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> Get(int id)
        {
            var task = await _projectTaskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<ProjectTask>> GetTasksForEmployee(long projectId, long employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var tasks = await _projectTaskService.GetProjectTasksForEmployeeAsync(projectId, employeeId);

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound("No tasks found for the specified user.");
            }

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTask>> Create(CreateProjectTaskDto dto)
        {
            var created = await _projectTaskService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.TaskId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProjectTaskDto dto)
        {
            var success = await _projectTaskService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _projectTaskService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }

}
