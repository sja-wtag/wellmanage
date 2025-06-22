using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.shared.Enums;

namespace wellmanage.shared.Models
{
    public class ProjectTaskDto
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public long AssignedToId { get; set; }
        public EmployeeDto? AssignedTo { get; set; }
        public long ProjectId { get; set; }
        public ProjectDto? Project { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
    }
}
