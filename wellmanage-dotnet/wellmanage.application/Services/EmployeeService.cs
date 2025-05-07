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
        public EmployeeService(IEmployeeRepository employeeRepository,IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEmployee(EmployeeSaveRequest request)
        {
            var user = await _userRepository.GetAsync(request.Id);
            var employee = _mapper.Map<Employee>(user);
            _mapper.Map(request, employee);
            await _employeeRepository.SaveAsync(employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
