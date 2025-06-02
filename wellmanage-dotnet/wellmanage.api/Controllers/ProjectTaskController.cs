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
    [Route("api/v{v:apiVersion}/project-task")]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskService _service;

        public ProjectTasksController(IProjectTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectTask>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> Get(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTask>> Create(CreateProjectTaskDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.TaskId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProjectTaskDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }

}
