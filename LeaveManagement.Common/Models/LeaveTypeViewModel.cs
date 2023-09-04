using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; } // can/should we create a base ViewModel class?

        // HTML helper DisplayNameFor will display Name from here. Also make it required (even if nullable in Db)
        [Display(Name = "Description")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Default Number Of Days")]
        [Required]
        [Range(1, 270, ErrorMessage = "Please Enter A Valid Number no greater than 270")]
        public int DefaultDays { get; set; }
    }
}