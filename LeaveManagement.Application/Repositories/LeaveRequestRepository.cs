﻿using AutoMapper;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Common.Models;
using LeaveManagement.Data;
// TODO: Resolve custom EmailSEnder in: using LeaveManagement.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _configurationProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly UserManager<Employee> _userManager;
        private readonly IEmailSender _emailSender;

        public LeaveRequestRepository(ApplicationDbContext context,
            IMapper mapper, 
            AutoMapper.IConfigurationProvider configurationProvider,
            IHttpContextAccessor httpContextAccessor,
            ILeaveAllocationRepository leaveAllocationRepository,
            UserManager<Employee> userManager,
            IEmailSender emailSender) : base(context)
        {
            this._context = context;
            this._mapper = mapper;
            this._configurationProvider = configurationProvider;
            this._httpContextAccessor = httpContextAccessor;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._userManager = userManager;
            this._emailSender = emailSender;
        }

        public async Task<List<LeaveRequest>> GetAllAsync(string employeeId)
        {
            return await _context.LeaveRequests.Where(q => q.RequestingEmployeeId == employeeId).ToListAsync();
        }

        public async Task<LeaveRequestViewModel?> GetLeaveRequestAsync(int? id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (leaveRequest == null)
            {
                return null;
            }

            var model = _mapper.Map<LeaveRequestViewModel>(leaveRequest);
            model.Employee = _mapper.Map<EmployeeListViewModel>(await _userManager.FindByIdAsync(leaveRequest?.RequestingEmployeeId));
            return model;
        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);

            var leaveAllocation = await _leaveAllocationRepository.GetEmployeeAllocation(user.Id, model.LeaveTypeId);

            if (leaveAllocation == null)
            {
                return false;
            }

            int daysRequested = (int)(model.EndDate.Value - model.StartDate.Value).TotalDays;

            if (daysRequested > leaveAllocation.NumberOfDays)
            {
                return false;
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;

            await AddAsync(leaveRequest);

            // Notify Employee via Email
            /* Disabling until Email server is working again and this can be tested */
            await _emailSender.SendEmailAsync(user.Email, "Leave Request Submitted Successfully", 
                $"Your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been submitted for approval");
            // */

            return true;
        }

        public async Task CancelLeaveRequest(int leaveRequestId, bool cancel=true)
        {
            var leaveRequest = await GetAsync(leaveRequestId);

            if (leaveRequest != null)
            {
                leaveRequest.Cancelled = cancel;
                await UpdateAsync(leaveRequest);
            }
        }

        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;

            if (approved)
            {
                var allocation = await _leaveAllocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequestId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                allocation.NumberOfDays -= daysRequested;

                // Prevent Number of days allocated from being negative to cover an edge case: if a request is created,
                //  and allocation is reduced before the request is approved
                if (allocation.NumberOfDays < 0) allocation.NumberOfDays = 0; 

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            await UpdateAsync(leaveRequest);

            // Notify Employee via email of change in Leave Request Approval Status
            var approvalStatus = approved ? "Approved" : "Declined";

            string htmlMessage = $"Your leave request from " +
                $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}";

            var user = await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);

            /* Disabling until Email server is working again and this can be tested
            await _emailSender.SendEmailAsync(user?.Email, $"Leave Request {approvalStatus}", htmlMessage); */
        }

        public async Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList()
        {
            var leaveRequests = await _context.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
            var model = new AdminLeaveRequestViewModel
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests),
            };

            foreach (var leaveRequest in model.LeaveRequests)
            {
                leaveRequest.Employee = _mapper.Map<EmployeeListViewModel>(await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
            }

            return model;
        }

        public async Task<EmployeeLeaveRequestViewModel> GetMyLeaveDetails()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);

            // NOTE: The object returned from async call is accessible outside the parens
            var allocations = (await _leaveAllocationRepository.GetEmployeeAllocations(user.Id)).LeaveAllocations; 

            var requests = _mapper.Map<List<LeaveRequestViewModel>>(await GetAllAsync(user.Id));

            // To display number of days requested
            foreach (var request in requests)
            {
                request.NumberOfDaysRequested = (int)(request.EndDate.Value - request.StartDate.Value).TotalDays;
            }

            return new EmployeeLeaveRequestViewModel(allocations, requests);
        }
    }
}
