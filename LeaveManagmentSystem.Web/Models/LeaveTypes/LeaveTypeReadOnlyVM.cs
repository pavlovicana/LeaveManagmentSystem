using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM : BaseLeaveTypeVM
    {
        public string Name { get; set; } = String.Empty;
        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}
