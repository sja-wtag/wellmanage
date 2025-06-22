using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using wellmanage.application.Interfaces;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Enums;
using wellmanage.shared.Models;

namespace wellmanage.application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectTaskService(IProjectTaskRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _projectTaskRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProjectTaskDto>> GetAllAsync()
        {
            var projects = await _projectTaskRepository.GetAllAsync();
            return (List<ProjectTaskDto>) _mapper.Map<List<ProjectTaskDto>>(projects);
        }

        public async Task<ProjectTaskDto> GetByIdAsync(long id)
        {
            var project = await _projectTaskRepository.GetAsync(id);
            return _mapper.Map<ProjectTaskDto>(project);
        }

        public async Task<ProjectTaskDto> CreateAsync(ProjectTaskDto dto)
        {
            var task = new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate ?? new DateTime(),
                IsCompleted = dto.IsCompleted,
                AssignedToId = dto.AssignedToId,
                ProjectId = dto.ProjectId,
                TaskStatus = dto.TaskStatus
            };
            await _projectTaskRepository.SaveAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> UpdateAsync(long id, ProjectTaskDto dto)
        {
            var task = await _projectTaskRepository.GetAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate ?? new DateTime();
            task.IsCompleted = dto.IsCompleted;
            task.AssignedToId = dto.AssignedToId;
            task.ProjectId = dto.ProjectId;
            task.TaskStatus = dto.TaskStatus;

            await _projectTaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var task = await _projectTaskRepository.GetAsync(id);
            if (task == null) return false;

            await _projectTaskRepository.DeleteAsync(task);
            return true;
        }

        public async Task<List<ProjectTaskDto>> GetTasksForEmployeeAsync(long employeeId)
        {
            var tasks = await _projectTaskRepository.GetAllTasksAssignedToEmployee(employeeId);
            
            return _mapper.Map<List<ProjectTaskDto>>(tasks);
        }

        public async Task<List<ProjectTaskDto>> GetAllTasksInAProjectAsync(long projectId)
        {
            var tasks = await _projectTaskRepository.GetAllTasksInAProject(projectId);

            return _mapper.Map<List<ProjectTaskDto>>(tasks);
        }

        public async Task<List<ProjectTaskDto>> GetProjectTasksForEmployeeAsync(long projectId, long employeeId)
        {  
            var tasks = await _projectTaskRepository.GetProjectBasedEmployeeAssignedTasks(projectId, employeeId);

            return _mapper.Map<List<ProjectTaskDto>>(tasks);
        }

        public async Task<List<ProjectTaskDto>> GetTasksByFiltersAsync(long? projectId, long? employeeId)
        {
            var tasks = await _projectTaskRepository.GetTasksByFiltersAsync(projectId, employeeId);
            return _mapper.Map<List<ProjectTaskDto>>(tasks);
        }

        public async Task UpdateTaskStatusAsync(long taskId, TaskStatusEnum taskStatus)
        {
            await _projectTaskRepository.UpdateTaskStatus(taskId, taskStatus);
        }
    }

}
