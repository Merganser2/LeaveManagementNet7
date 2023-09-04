using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;

namespace LeaveManagement.Application.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        { 
        }
    }
}
