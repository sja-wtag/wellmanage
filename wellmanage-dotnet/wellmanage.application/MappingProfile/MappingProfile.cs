using AutoMapper;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Attendance, AttendanceResponse>().ReverseMap();
            CreateMap<EmployeeSaveRequest, Employee>()
              .ForMember(dest => dest.Projects,
                  opt => opt.MapFrom(src => MapProjectIdsToProjects(src.Projects)))
              .ForMember(dest => dest.Assignees,
                  opt => opt.MapFrom(src => MapAssignyIdsToEmployees(src.Assignies)));

            // Mapping: Employee => EmployeeSaveRequest
            CreateMap<Employee, EmployeeSaveRequest>()
                .ForMember(dest => dest.Projects,
                    opt => opt.MapFrom(src => MapProjectsToIds(src.Projects)))
                .ForMember(dest => dest.Assignies,
                    opt => opt.MapFrom(src => MapEmployeesToIds(src.Assignees)));
            CreateMap<Project, ProjectDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProjectId));

            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Employees, opt => opt.Ignore()); 
        }

        private static List<Project> MapProjectIdsToProjects(List<long>? projectIds)
        {
            return projectIds?.Select(id => new Project { ProjectId = id }).ToList() ?? new List<Project>();
        }

        private static List<long> MapProjectsToIds(List<Project>? projects)
        {
            return projects?.Select(p => p.ProjectId).ToList() ?? new List<long>();
        }

        private static List<Employee> MapAssignyIdsToEmployees(List<long>? employeeIds)
        {
            return employeeIds?.Select(id => new Employee { Id = id }).ToList() ?? new List<Employee>();
        }

        private static List<long> MapEmployeesToIds(List<Employee>? employees)
        {
            return employees?.Select(e => e.Id).ToList() ?? new List<long>();
        }
    }
}
