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
            CreateMap<User, Employee>().ReverseMap();
            CreateMap<Employee, EmployeeSaveRequest>();
        }
    }
}
