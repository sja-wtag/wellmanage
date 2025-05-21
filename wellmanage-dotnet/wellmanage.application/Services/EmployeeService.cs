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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEmployee(EmployeeSaveRequest request)
        {
            try
            {
                var user = await _userRepository.GetAsync(request.UserId);
                var employee = new Employee();
                _mapper.Map(request, employee);
                employee.User = user;
                foreach (long id in request.Assignies) 
                {
                    var assignee = await _employeeRepository.GetAsync(id);
                    employee.Assignees.Add(assignee);
                }
                await _employeeRepository.SaveAsync(employee);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeeDto>> GetEmployeesWithUserInformation()
        {
            var employees = await _employeeRepository.GetEmployeesWithUserInformation();
            return (List<EmployeeDto>)employees;
        }
    }
}
