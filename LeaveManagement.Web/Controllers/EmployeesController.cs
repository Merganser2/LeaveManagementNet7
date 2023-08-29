using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public EmployeesController(UserManager<Employee> userManager, IMapper mapper, 
                                    ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        // GET: EmployeesController
        public async Task<ActionResult> Index()
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
            var model = _mapper.Map<List<EmployeeListViewModel>>(employees);

            return View(model);
        }

        // GET: EmployeesController/ViewAllocations/employeeId
        public async Task<IActionResult> ViewAllocations(string id)
        {
            var model = await _leaveAllocationRepository.GetEmployeeAllocations(id);
            return View(model);
        }

        // GET: EmployeesController/EditAllocation/5
        public async Task<IActionResult> EditAllocation(int id)
        {
            var model = await _leaveAllocationRepository.GetAllocationDetails(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: EmployeesController/EditAllocation/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // NOTE: At 7:48 in 48. he changes 2nd param from IFormCollection collection to...
        //      How does this work, ie how does that object come in? Same pattern in LeaveTypesController, but didn't notice it at the time. Revisit....
        //      Does it get shared via the GET version?
        public async Task<IActionResult> EditAllocation(int id, LeaveAllocationEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _leaveAllocationRepository.UpdateEmployeeAllocation(model))
                    { 
                        return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId});
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(String.Empty, "An error has occurred.");
            }

            // Trying to fully grok this: we have to get this information for the View in case the Edit fails
            model.Employee = _mapper.Map<EmployeeListViewModel>(await _userManager.FindByIdAsync(model.EmployeeId));
            model.LeaveType = _mapper.Map<LeaveTypeViewModel>(await _leaveTypeRepository.GetAsync(model.LeaveTypeId));

            return View(); // Will display any Model State errors that arose
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
