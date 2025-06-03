using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;

namespace wellmanage.data.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        private readonly DataContext _databaseContext;
        public ProjectTaskRepository(DataContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<ProjectTask>> GetProjectBasedEmployeeAssignedTasks(long projectId, long employeeId)
        {
            return await _databaseContext.ProjectTasks.Where(task => task.ProjectId == projectId && task.AssignedToId == employeeId).ToListAsync();
        }
    }
}
