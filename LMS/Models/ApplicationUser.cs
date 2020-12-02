using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime TimeOfRegistration { get; set; }

        // Foreign key
        [Display(Name = "Course")]
        public int? CourseId { get; set; }

        // Navigation property
        public Course Course { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}
