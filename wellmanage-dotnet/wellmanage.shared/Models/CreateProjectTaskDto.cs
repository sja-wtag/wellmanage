using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.shared.Enums;

namespace wellmanage.shared.Models
{
    public class CreateProjectTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public long AssignedToId { get; set; }
        public long ProjectId { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
    }
}
