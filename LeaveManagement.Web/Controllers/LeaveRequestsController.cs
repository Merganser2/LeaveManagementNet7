using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Common.Constants;

namespace LeaveManagement.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILogger<LeaveRequestsController> _logger;

        public LeaveRequestsController(ILeaveRequestRepository requestRepository, ILeaveTypeRepository leaveTypeRepository, 
                                       ILogger<LeaveRequestsController> logger)
        {
            _leaveRequestRepository = requestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestRepository.GetAdminLeaveRequestList(); 
            return View(model);
        }

        public async Task<ActionResult> MyLeave()
        {
            var model = await _leaveRequestRepository.GetMyLeaveDetails();
            return View(model);
        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestAsync(id);

            if (leaveRequest is null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // GET: LeaveRequests/Create
        public async Task<IActionResult> Create()
        {
            var model = new LeaveRequestCreateViewModel
            {
                LeaveTypes = new SelectList(await _leaveTypeRepository.GetAllAsync(), "Id", "Name")

            };

            return View(model);
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isValid = await _leaveRequestRepository.CreateLeaveRequest(model);

                    if (isValid)
                    {
                        return RedirectToAction(nameof(MyLeave));
                    }
                    ModelState.AddModelError(String.Empty, "You have exceeded your allocation for this Leave Type.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating Leave Request");

                ModelState.AddModelError(String.Empty, "An error has occurred.");
            }

            model.LeaveTypes = new SelectList(await _leaveTypeRepository.GetAllAsync(), "Id", "Name", model.LeaveTypeId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, bool cancel)
        {
            try
            {
                await _leaveRequestRepository.CancelLeaveRequest(id, cancel); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Cancelling Leave Request");
                throw;
            }
            return RedirectToAction(nameof(MyLeave));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await _leaveRequestRepository.ChangeApprovalStatus(id, approved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Approving or Rejecting Leave Request");
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
