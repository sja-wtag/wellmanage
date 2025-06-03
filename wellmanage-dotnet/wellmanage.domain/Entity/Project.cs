using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.domain.Entity
{
    public class Project
    {
        [Key]
        public long ProjectId { get; set; }
        public required string Name { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
