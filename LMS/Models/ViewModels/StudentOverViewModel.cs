using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModels
{
    public class StudentOverViewModel
    {
        // Lists
        public List<Course> Courses { get; set; }
        // fields
        public ApplicationUser Student { get; set; }
        public string NextCourse { get; set; }
        public bool ShowAllCourses { get; set; }

    }
}
