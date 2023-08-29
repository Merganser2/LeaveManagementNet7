namespace LeaveManagement.Web.Models
{
    public class EmployeeAllocationViewModel : EmployeeListViewModel
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
    }
}
