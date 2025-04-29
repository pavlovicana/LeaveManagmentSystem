using LeaveManagmentSystem.Web.Common;
using LeaveManagmentSystem.Web.Models.LeaveAllocations;
using LeaveManagmentSystem.Web.Services;
using LeaveManagmentSystem.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace LeaveManagmentSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationsService, ILeaveTypesServices _leaveTypesService ) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employees = await _leaveAllocationsService.GetEmployees();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id)
        {
            await _leaveAllocationsService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        public async Task<IActionResult> Details(string? userId)
        {
            var employeeVM = await _leaveAllocationsService.GetEmployeeLeaveAllocations(userId);
            return View(employeeVM);
        }
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allocation = await _leaveAllocationsService.GetEmployeeAllocation(id.Value);
            if (allocation == null)
            {
                return NotFound();
            }
            return View(allocation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
        {
            if (await  _leaveTypesService.DaysExceedMaximum(allocation.LeaveType.Id, allocation.DaysAllocated))
            {
                ModelState.AddModelError("DaysAllocated", "The allocation exceeds the maximum leave type value");
            }
            if (ModelState.IsValid)
            {
                await _leaveAllocationsService.EditAllocation(allocation);
                return RedirectToAction(nameof(Details), new { userId = allocation.Employee.Id });
            }

            var days = allocation.DaysAllocated;
            allocation = await _leaveAllocationsService.GetEmployeeAllocation(allocation.Id);
            allocation.DaysAllocated = days;
            return View(allocation);

        }
    }
}
