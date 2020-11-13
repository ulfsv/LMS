using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign key
        public int CourseId { get; set; }

        // Navigation property
        public Course Course { get; set; }

        public ICollection<Aktivitet> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}
