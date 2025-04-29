using LeaveManagmentSystem.Web.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Web.Data
{
    public class LeaveRequestStatus : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }


    }
}