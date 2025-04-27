using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.domain.Entity
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
      
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }

}
