using System.ComponentModel.DataAnnotations;

namespace LMS.Models.ViewModels
{
    public class AddNewActivityViewModel
    {
        public int ActivityId { get; set; }
        [Required, StringLength(30, ErrorMessage = "30 characters maximum"), Display(Name = "Custom Activity Type")]
        public string CustomActivityType { get; set; }
    }
}
