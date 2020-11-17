using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModels
{
    public class TeacherOverViewModel
    {
        // Lists
        public List<Course> Courses { get; set; }
        public List<Module> Modules { get; set; }
        public List<Aktivitet> Activities { get; set; }
        public List<ApplicationUser> Students { get; set; }
        public List<Document> Documents { get; set; }
        // fields
        public ApplicationUser Teacher { get; set; }
        public string NextCourse { get; set; }

    }
}
