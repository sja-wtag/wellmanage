using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.shared.Models
{
    public class EmployeeSaveRequest
    {
        public long UserId { get; set; }
        public string? Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Designation { get; set; }
        public long? TeamLeadId { get; set; }
    }
}
