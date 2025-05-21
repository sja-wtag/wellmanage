using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.domain.Entity;
using wellmanage.shared.Models;

namespace wellmanage.data.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<EmployeeDto>> GetEmployeesWithUserInformation();
    }
}
