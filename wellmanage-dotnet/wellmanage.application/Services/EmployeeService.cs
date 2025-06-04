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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository,IProjectRepository projectRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEmployee(EmployeeSaveRequest request)
        {
            try
            {
                var user = await _userRepository.GetAsync(request.UserId);
                if (user == null)
                    throw new Exception($"User with ID {request.UserId} not found.");


                var employee = new Employee
                {
                    UserId = request.UserId,
                    Department = request.Department,
                    JoiningDate = request.JoiningDate,
                    Designation = request.Designation,
                    TeamLeadId = request.TeamLeadId,
                    User = user
                };

                if (request.Assignies != null && request.Assignies.Any())
                {
                    foreach (var assigneeId in request.Assignies)
                    {
                        var assignee = new Employee { Id = assigneeId };
                        _employeeRepository.Attach(assignee); 
                        employee.Assignees.Add(assignee);
                    }
                }


                if (request.Projects != null && request.Projects.Any())
                {
                    foreach (var projectId in request.Projects)
                    {
                        var project = new Project { ProjectId = projectId };
                        _projectRepository.Attach(project); 
                        employee.Projects.Add(project);
                    }
                }


                await _employeeRepository.SaveAsync(employee);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while adding employee.", ex);
            }
        }



        public async Task<List<EmployeeDto>> GetEmployeesWithUserInformation()
        {
            var employees = await _employeeRepository.GetEmployeesWithUserInformation();
            return (List<EmployeeDto>)employees;
        }
    }
}
