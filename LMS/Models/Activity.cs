using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Foreign key
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }

        // Navigation property

        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }

        public ICollection<Document> Documents { get; set; }

    }
}
