using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _configurationProvider;
        private readonly IEmailSender _emailSender;

        public LeaveAllocationRepository(ApplicationDbContext context, UserManager<Employee> userManager, 
                                         ILeaveTypeRepository leaveTypeRepository, 
                                         IMapper mapper, AutoMapper.IConfigurationProvider configurationProvider,
                                         IEmailSender emailSender
                                         ) : base(context)
        {
            this._context = context;
            this._userManager = userManager;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._configurationProvider = configurationProvider;
            this._emailSender = emailSender;
        }

        // Allocate Leave for all Employees
        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepository.GetAsync(leaveTypeId);

            var leaveAllocations = new List<LeaveAllocation>();
            var employeesWithNewAllocations = new List<Employee>();

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
                    employeesWithNewAllocations.Add(employee);
                }
            }

            await AddRangeAsync(leaveAllocations);

            /* Disabling until Email Server is working again for this to be tested
            foreach (var employee in employeesWithNewAllocations)
            {
                await _emailSender.SendEmailAsync(employee.Email, $"Leave Allocation Posted for {period}", $"Your {leaveType.Name} " +
                    $"has been posted for the period of {period}. You have been given {leaveType.DefaultDays}.");
            }*/
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int Period)
        {
            return await _context.LeaveAllocations.AnyAsync(l => l.EmployeeId == employeeId 
                                                                 && l.LeaveTypeId  == leaveTypeId
                                                                 && l.Period == Period);
        }

        // Get all Leave Allocations for this employee's ID
        public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await _context.LeaveAllocations
                                  .Include(q => q.LeaveType) // Get additional info we need from LeaveType table, like DefaultDays (like an inner join)
                                  .Where(q => q.EmployeeId == employeeId)
                                  .ProjectTo<LeaveAllocationViewModel>(_configurationProvider)
                                  .ToListAsync();
            var employee = await _userManager.FindByIdAsync(employeeId);
            var employeeAllocationModel = _mapper.Map<EmployeeAllocationViewModel>(employee);

            // By using ProjectTo we avoid having to do: _mapper.Map<List<LeaveAllocationViewModel>>(allocations);
            employeeAllocationModel.LeaveAllocations = allocations; 

            return employeeAllocationModel;
        }

        public async Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId );
        }

        // Get this Allocation with all the details. Trevoir named this GetEmployeeAllocation
        public async Task<LeaveAllocationEditViewModel> GetAllocationDetails(int id)
        {
            var allocation = await _context.LeaveAllocations
                                  .Include(q => q.LeaveType) // Get additional info we need from LeaveType table, like DefaultDays (like an inner join)
                                  .FirstOrDefaultAsync(q => q.Id == id);
            if (allocation == null) return null;

            var allocationEditModel = _mapper.Map<LeaveAllocationEditViewModel>(allocation);

            var employee = await _userManager.FindByIdAsync(allocation.EmployeeId);

            allocationEditModel.Employee = _mapper.Map<EmployeeAllocationViewModel>(employee);

            return allocationEditModel;
        }

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditViewModel model)
        {
            var leaveAllocation = await GetAsync(model.Id);

            if (leaveAllocation == null) return false;

            leaveAllocation.Period = model.Period;
            leaveAllocation.NumberOfDays = model.NumberOfDays;
            await UpdateAsync(leaveAllocation);

            return true;
        }
    }
}
