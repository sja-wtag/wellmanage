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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly DataContext _dataContext;
        public ProjectRepository(DataContext databaseContext) : base(databaseContext)
        {
            _dataContext = databaseContext;
        }

        public async Task<List<Project>> GetProjectsByEmployeeIdAsync(long employeeId)
        {
            return await _dataContext.Projects
                .Where(p => p.Employees.Any(e => e.Id == employeeId))
                .ToListAsync();
        }
    }
}
