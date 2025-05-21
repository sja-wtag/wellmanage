using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.shared.Models
{
    public class AttendanceDto
    {
        public int Id { get; set; }

        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        [Browsable(false)]
        public long UserId { get; set; }
        public string Username { get; set; }
    }

}
