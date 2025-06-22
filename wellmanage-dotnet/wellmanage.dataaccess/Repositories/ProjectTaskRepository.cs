using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Enums;

namespace wellmanage.data.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        private readonly DataContext _databaseContext;
        public ProjectTaskRepository(DataContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<ProjectTask>> GetAllTasksAssignedToEmployee(long employeeId)
        {
            return await _databaseContext.ProjectTasks.Where(task => task.AssignedToId == employeeId).ToListAsync();
        }
        public async Task<List<ProjectTask>> GetAllTasksInAProject(long projectId)
        {
            return await _databaseContext.ProjectTasks.Where(task => task.ProjectId == projectId).ToListAsync();
        }

        public async Task<List<ProjectTask>> GetProjectBasedEmployeeAssignedTasks(long projectId, long employeeId)
        {
            return await _databaseContext.ProjectTasks.Where(task => task.ProjectId == projectId && task.AssignedToId == employeeId).ToListAsync();
        }

        public async Task<List<ProjectTask>> GetTasksByFiltersAsync(long? projectId, long? employeeId)
        {
            var query = _databaseContext.ProjectTasks.AsQueryable();

            if (projectId.HasValue)
            {
                query = query.Where(task => task.ProjectId == projectId.Value);
            }

            if (employeeId.HasValue)
            {
                query = query.Where(task => task.AssignedToId == employeeId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateTaskStatus(long taskId, TaskStatusEnum taskStatus)
        {
            await _databaseContext.ProjectTasks.AsNoTracking()
                .Where(task => task.TaskId == taskId)
                .ExecuteUpdateAsync(task => task.SetProperty(p => p.TaskStatus, taskStatus));
        }
    }
}
