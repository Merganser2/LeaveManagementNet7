using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel model);
        Task<List<LeaveRequest>> GetAllAsync(string employeeId);
//        Task<LeaveRequestViewModel?> GetLeaveRequestAsync(int? id);  
        Task<EmployeeLeaveRequestViewModel> GetMyLeaveDetails();
        Task CancelLeaveRequest(int leaveRequestId, bool cancel=false);
        Task ChangeApprovalStatus(int leaveRequestId, bool approved);
        Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList();
    }
}
