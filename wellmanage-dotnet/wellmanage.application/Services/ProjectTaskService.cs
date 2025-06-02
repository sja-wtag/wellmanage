using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.application.Interfaces;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTaskService(IProjectTaskRepository repository, IUnitOfWork unitOfWork)
        {
            _projectTaskRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProjectTask>> GetAllAsync()
        {
            return (List<ProjectTask>)await _projectTaskRepository.GetAllAsync();
        }

        public async Task<ProjectTask> GetByIdAsync(int id)
        {
            return await _projectTaskRepository.GetAsync(id);
        }

        public async Task<ProjectTask> CreateAsync(CreateProjectTaskDto dto)
        {
            var task = new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
                AssignedToId = dto.AssignedToId,
                ProjectId = dto.ProjectId,
                TaskStatus = dto.TaskStatus
            };
            await _projectTaskRepository.SaveAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(int id, CreateProjectTaskDto dto)
        {
            var task = await _projectTaskRepository.GetAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;
            task.AssignedToId = dto.AssignedToId;
            task.ProjectId = dto.ProjectId;
            task.TaskStatus = dto.TaskStatus;

            await _projectTaskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _projectTaskRepository.GetAsync(id);
            if (task == null) return false;

            await _projectTaskRepository.DeleteAsync(task);
            return true;
        }
    }

}
