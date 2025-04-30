using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.shared.Models
{
    public class AttendanceStatus
    {
        public DateTime? LastCheckInAt { get; set; }
        public DateTime? LastCheckOutAt { get; set; }
        public bool IsAlreadyCheckedIn => LastCheckInAt.HasValue;
        public bool IsAlreadyCheckedOut => LastCheckOutAt.HasValue;
        public TimeSpan TotalWorkTime { get; set; }
    }

}
