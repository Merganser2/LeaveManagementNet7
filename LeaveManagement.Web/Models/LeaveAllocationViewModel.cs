using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Web.Models
{
    public class LeaveAllocationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }

        [Display(Name = "Allocation Period")]
        public int Period { get; set; }

        // Important: Using View Model for LeaveType, NOT the data type, as we are within a View Model type
        public LeaveTypeViewModel LeaveType { get; set; }
    }
}