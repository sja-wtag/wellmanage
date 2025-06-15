using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using wellmanage.application.Interfaces;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            await _projectRepository.SaveAsync(project);
            await _unitOfWork.SaveChangesAsync();
            return project;
        }

        public async Task<Project> GetProjectByIdAsync(long id)
        {
            return await _projectRepository.GetAsync(id);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            _projectRepository.UpdateAsync(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(long id)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project == null) throw new KeyNotFoundException($"Project with id {id} not found.");

            _projectRepository.DeleteAsync(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}