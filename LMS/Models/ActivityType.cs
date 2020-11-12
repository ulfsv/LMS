using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
