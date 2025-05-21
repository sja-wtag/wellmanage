using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.application.Interfaces
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeSaveRequest request);
        Task<List<EmployeeDto>> GetEmployeesWithUserInformation();
    }
}
