using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wellmanage.application.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Enums;
using wellmanage.shared.Models;

namespace wellmanage_dotnet.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/tasks")]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTasksController(IProjectTaskService service)
        {
            _projectTaskService = service;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<ProjectTask>>> GetAll()
        //{
        //    return Ok(await _projectTaskService.GetAllAsync());
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> Get(long id)
        {
            var task = await _projectTaskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ProjectTaskDto>>> GetTasks([FromQuery] long? projectId, [FromQuery] long? employeeId)
        {
            var tasks = await _projectTaskService.GetTasksByFiltersAsync(projectId, employeeId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTaskDto>> Create(ProjectTaskDto dto)
        {
            var created = await _projectTaskService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.TaskId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, ProjectTaskDto dto)
        {
            var success = await _projectTaskService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _projectTaskService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpPut("{taskId}/status")]
        public async Task<IActionResult> UpdateStatus(long taskId, [FromBody] TaskStatusEnum newStatus)
        {
            await _projectTaskService.UpdateTaskStatusAsync(taskId, newStatus);
            return NoContent();
        }
    }
}
