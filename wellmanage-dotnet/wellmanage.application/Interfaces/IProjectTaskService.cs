using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Enums;
using wellmanage.shared.Models;

namespace wellmanage.application.Interfaces
{
    public interface IProjectTaskService
    {
        Task<List<ProjectTaskDto>> GetAllAsync();
        Task<ProjectTaskDto> GetByIdAsync(long id);
        Task<ProjectTaskDto> CreateAsync(ProjectTaskDto dto);
        Task<bool> UpdateAsync(long id, ProjectTaskDto dto);
        Task<bool> DeleteAsync(long id);
        Task<List<ProjectTaskDto>> GetTasksForEmployeeAsync(long employeeId);
        Task<List<ProjectTaskDto>> GetProjectTasksForEmployeeAsync(long projectId, long employeeId);
        Task<List<ProjectTaskDto>> GetAllTasksInAProjectAsync(long projectId);
        Task<List<ProjectTaskDto>> GetTasksByFiltersAsync(long? projectId, long? employeeId);
        Task UpdateTaskStatusAsync(long taskId, TaskStatusEnum taskStatus);
    }

}
