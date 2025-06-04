using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;

namespace wellmanage.data.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext databaseContext) : base(databaseContext)
        {
        }
    }
}
