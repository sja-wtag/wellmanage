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
            CreateMap<Employee, EmployeeSaveRequest>().ReverseMap();
                
        }

        private List<Employee> MapAssignies(List<long> assignyIds)
        {
            if (assignyIds == null || assignyIds.Count == 0)
                return new List<Employee>();

            return assignyIds.Select(id => new Employee { Id = id }).ToList();
        }
    }
}
