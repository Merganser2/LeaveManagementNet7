using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        public Task LeaveAllocation(int id);
        public Task<bool> AllocationExists(string employeeId, int leaveTypeId, int Period);
        Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId);
    }
}
