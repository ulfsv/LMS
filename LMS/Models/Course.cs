using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Ulf added validation row 16 datatype.text, row 18 display name on start date, row 19 datatype Date

namespace LMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date )]
        public DateTime StartDate { get; set; }

        // Navigation property
        public ICollection<Module> Modules { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
