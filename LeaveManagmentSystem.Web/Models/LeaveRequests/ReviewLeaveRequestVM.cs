using LeaveManagmentSystem.Web.Models.LeaveAllocations;
using LeaveManagmentSystem.Web.Services.LeaveRequests;

namespace LeaveManagmentSystem.Web.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM Employee { get; set; } = new EmployeeListVM();

        [Display(Name = "Additional information")]
        public string RequestComments { get; set; }
    }
}