using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task LeaveAllocation(int id);
        Task<bool> AllocationExists(string employeeId, int leaveTypeId, int Period);
        Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId);
        Task<LeaveAllocationEditViewModel> GetAllocationDetails(int id);
        Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditViewModel model);
    }
}
