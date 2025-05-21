using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.domain.Entity
{
    public class Employee
    {
        public Employee() { }

        [Key]
        public long Id { get; set; }

        public string? Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Designation { get; set; }

        [ForeignKey("TeamLead")]
        public long? TeamLeadId { get; set; }

        [InverseProperty("Assignees")]
        public Employee? TeamLead { get; set; }

        [InverseProperty("TeamLead")]
        public List<Employee> Assignees { get; set; } = new List<Employee>();

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }

}
