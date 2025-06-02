using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.shared.Enums;

namespace wellmanage.domain.Entity
{
    public class ProjectTask
    {
        [Key] public int TaskId { get; set; }

        [MaxLength(50, ErrorMessage = "Title cannot be more than 50 characters.")]
        public string Title { get; set; }

        [MaxLength(8000, ErrorMessage = "Description cannot be more than 8000 characters.")]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey("AssignedTo")]
        public long AssignedToId { get; set; }
        public Employee AssignedTo { get; set; }


        [ForeignKey("Project")]
        public long ProjectId { get; set; }
        public Project Project { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
    }

}
