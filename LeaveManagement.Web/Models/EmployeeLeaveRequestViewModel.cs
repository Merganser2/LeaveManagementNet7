using LeaveManagement.Web.Data;

namespace LeaveManagement.Web.Models
{
    public class EmployeeLeaveRequestViewModel
    {
        public EmployeeLeaveRequestViewModel(List<LeaveAllocationViewModel> leaveAllocations, List<LeaveRequestViewModel> leaveRequests)
        {
            LeaveAllocations = leaveAllocations;
            LeaveRequests = leaveRequests;
        }
 
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    }
}