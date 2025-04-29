using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagmentSystem.Web.Data
{
    public class LeaveAllocation : BaseEntity
    {
        public LeaveType? LeaveType { get; set; }    

        public int LeaveTypeId { get; set; }

        [ForeignKey("EmployeeId")]
        public ApplicationUser? Employee { get; set; }
        public string EmployeeId { get; set; }

        public Period? Period { get; set; }
        public int PeriodId { get; set; }

        public int DaysAllocated { get; set; }  

    }
}
