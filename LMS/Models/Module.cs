using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Module
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }

        // Foreign key
        [DisplayName("Course")]
        public int CourseId { get; set; }

        // Navigation property
        public Course Course { get; set; }

        public ICollection<Aktivitet> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}
