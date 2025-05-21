using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        public EmployeeRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public async Task<List<EmployeeDto>> GetEmployeesWithUserInformation()
        {
            var employees = await _dataContext.Employees.Include(emp => emp.User).Include(emp=> emp.TeamLead).Select(emp => new EmployeeDto()
            {
                Id = emp.Id,
                Name = emp.User.FullName,
                Department = emp.Department,
                Designation = emp.Designation,
                JoiningDate = emp.JoiningDate,
                TeamLeadId = emp.TeamLeadId,
                UserId = emp.UserId,
                TeamLead = new EmployeeDto()
                {
                    Id = emp.Id,
                    Name = emp.User.FullName
                },
                User = new UserInfo()
                {
                    Id = emp.User.Id,
                    FullName = emp.User.FullName,
                    Email = emp.User.Email
                }
            }).ToListAsync();
            return employees;
        }
    }
}
