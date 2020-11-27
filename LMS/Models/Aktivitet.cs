using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Aktivitet
    {
        public int Id { get; set; }


        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }

        // Foreign key
        [DisplayName("Module")]
        public int ModuleId { get; set; }

        [DisplayName("Activity Type")]
        public int ActivityTypeId { get; set; }

        // Navigation property

        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }

        public ICollection<Document> Documents { get; set; }

    }
}
