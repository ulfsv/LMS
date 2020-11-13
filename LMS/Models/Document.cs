using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadTimeStamp { get; set; }
        public string Storage { get; set; }

        // Foreign key
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
        public string ApplicationUserId { get; set; }

        // Navigation property
        public Course Course { get; set; }
        public Module Module { get; set; }
        public Aktivitet Activity { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
