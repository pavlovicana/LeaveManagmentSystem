using LeaveManagmentSystem.Web.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LeaveManagmentSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Apply the configurations for the entities
           /* builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());*/
        }
        //Create a DbSet property for LeaveType  
        public DbSet<LeaveType> LeaveTypes { get; set; } // LeaveType is a model class 

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; } 
        public DbSet<Period> Periods { get; set; }

        public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }


    } 
}
