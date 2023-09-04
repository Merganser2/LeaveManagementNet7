using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Common.Models
{
    public class LeaveAllocationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Number of Days Allocated")]
        [Range(1, 365, ErrorMessage = "Invalid Number of Days")]
        public int NumberOfDays { get; set; }

        [Required]
        [Display(Name = "Allocation Period")]
        public int Period { get; set; }

        // Important: Using View Model for LeaveType, NOT the data type, as we are within a View Model type
        public LeaveTypeViewModel? LeaveType { get; set; }
    }
}