using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace wellmanage.shared.Models
{
    public class EmployeeDto
    {
        [Browsable(false)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Designation { get; set; }
        [Browsable(false)]
        public long? TeamLeadId { get; set; }
        public EmployeeDto? TeamLead { get; set; }
        public List<EmployeeDto> Assignies { get; set; } = new List<EmployeeDto>();
        [Browsable(false)]
        public long UserId { get; set; }
        [Browsable(false)]
        public UserInfo User { get; set; }
        public string Email => User?.Email;

        public override string ToString()
        {
            return Name;
        }
    }
}

