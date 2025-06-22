using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.application.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> GetProjectByIdAsync(long id);
        Task<List<Project>> GetProjectsByEmployeeIdAsync(long id);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(long id);
    }
}
