using LeaveManagmentSystem.Web.Models.LeaveTypes;
using LeaveManagmentSystem.Web.Models.Periods;
using System.Diagnostics.Contracts;

namespace LeaveManagmentSystem.Web.Models.LeaveAllocations
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }

        [Display(Name = "Number of Days")]
        public int DaysAllocated { get; set; }

        [Display(Name = "Allocation Period")]
        public PeriodVM Period { get; set; } = new PeriodVM();

        public LeaveTypeReadOnlyVM LeaveType { get; set; } = new LeaveTypeReadOnlyVM(); 
    }
}
