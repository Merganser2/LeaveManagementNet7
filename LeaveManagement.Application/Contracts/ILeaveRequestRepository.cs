using LeaveManagement.Data;
using LeaveManagement.Common.Models;

namespace LeaveManagement.Application.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel model);
        Task<List<LeaveRequest>> GetAllAsync(string employeeId);
        Task<LeaveRequestViewModel?> GetLeaveRequestAsync(int? id);  
        Task<EmployeeLeaveRequestViewModel> GetMyLeaveDetails();
        Task CancelLeaveRequest(int leaveRequestId, bool cancel=false);
        Task ChangeApprovalStatus(int leaveRequestId, bool approved);
        Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList();
    }
}
