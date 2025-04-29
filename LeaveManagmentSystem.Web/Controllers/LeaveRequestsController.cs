using LeaveManagmentSystem.Web.Models.LeaveRequests;
using LeaveManagmentSystem.Web.Services;
using LeaveManagmentSystem.Web.Services.LeaveRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagmentSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController(ILeaveTypesServices _leaveTypesServices, ILeaveRequestsService _leaveRequestsService) : Controller
    {
        //Employee View Requests
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestsService.GetEmployeeLeaveRequests();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            //Get Leave Types
            var leaveTypes = await _leaveTypesServices.GetAll();

            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList,


            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            //Validate that the days don't exceed the maximum days
            if(await _leaveRequestsService.RequestDatesExceedAllocation(model))
            {
                ModelState.AddModelError(nameof(model.EndDate), "Number of days requested is invalid.");
            }   
            if (ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index)); 
            }
            var leaveTypes = await _leaveTypesServices.GetAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
            model.LeaveTypes = leaveTypesList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await _leaveRequestsService.CancelLeaveRequest(id);
            return RedirectToAction(nameof(Index));
        }

        //Admin / Supervisor View Requests
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestsService.AdminGetAllLeaveRequests();
            return View(model);
        }

        //Admin / Supervisor View Requests
        public async Task<IActionResult> Review(int id)
        {
            var model = await _leaveRequestsService.GetLeaveRequestForReview(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Admin / Supervisor View Requests
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
    }
}
