using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.domain.Entity
{
    public class Employee : User
    {
        public string? Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Designation { get; set; }
        [ForeignKey("TeamLead")]
        public long TeamLeadId {  get; set; }
        public Employee TeamLead { get; set; }
        public List<Employee> Assignies { get; set; } = new List<Employee>();
    }
}
