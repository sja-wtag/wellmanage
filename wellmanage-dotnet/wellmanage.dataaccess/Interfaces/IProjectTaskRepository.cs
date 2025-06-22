using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Enums;

namespace wellmanage.data.Interfaces
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        Task<List<ProjectTask>> GetProjectBasedEmployeeAssignedTasks(long projectId, long assignedUserId);
        Task<List<ProjectTask>> GetAllTasksAssignedToEmployee(long employeeId);
        Task<List<ProjectTask>> GetAllTasksInAProject(long projectId);
        Task<List<ProjectTask>> GetTasksByFiltersAsync(long? projectId, long? employeeId);
        Task UpdateTaskStatus(long taskId, TaskStatusEnum taskStatus);
    }
}
