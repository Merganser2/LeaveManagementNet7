using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveAllocationRepository(ApplicationDbContext context, UserManager<Employee> userManager, 
                                         ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : base(context)
        {
            this._context = context;
            this._userManager = userManager;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepository.GetAsync(leaveTypeId);

            var leaveAllocations = new List<LeaveAllocation>();

            foreach (var employee in employees)
            {
                if (!await AllocationExists(employee.Id, leaveTypeId, period))
                {
                    leaveAllocations.Add(new LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveTypeId,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }
            }

            await AddRangeAsync(leaveAllocations);
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int Period)
        {
            return await _context.LeaveAllocations.AnyAsync(l => l.EmployeeId == employeeId 
                                                                 && l.LeaveTypeId  == leaveTypeId
                                                                 && l.Period == Period);
        }

        public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await _context.LeaveAllocations
                                  .Include(q => q.LeaveType) // Get additional info we need from LeaveType table, like DefaultDays (like an inner join)
                                  .Where(q => q.EmployeeId == employeeId).ToListAsync();
            var employee = await _userManager.FindByIdAsync(employeeId);
            var employeeAllocationModel = _mapper.Map<EmployeeAllocationViewModel>(employee);
            employeeAllocationModel.LeaveAllocations = _mapper.Map<List<LeaveAllocationViewModel>>(allocations);

            return employeeAllocationModel;
        }
    }
}
