using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.application.Interfaces
{
    public interface IProjectTaskService
    {
        Task<List<ProjectTask>> GetAllAsync();
        Task<ProjectTask> GetByIdAsync(int id);
        Task<ProjectTask> CreateAsync(CreateProjectTaskDto dto);
        Task<bool> UpdateAsync(int id, CreateProjectTaskDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<ProjectTask>> GetProjectTasksForEmployeeAsync(long projectId, long employeeId);
    }

}
