 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "4cc9ef1b-224d-4718-8119-54039cbd8fe0",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",

                },
                new IdentityRole
                {
                    Id = "4a154e49-6b8c-4b8e-851c-3ed673b19eec",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole
                {
                    Id = "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });

            var hasher = new PasswordHasher<ApplicationUser>();
            //default user 
            //create a default user with the password P@ssword1
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "d1aefdfd-eeb8-4998-a417-3b5d5989f244",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(1994, 5, 21)
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27",
                UserId = "d1aefdfd-eeb8-4998-a417-3b5d5989f244"
            });
        }
        //Create a DbSet property for LeaveType  
        public DbSet<LeaveType> LeaveTypes { get; set; } // LeaveType is a model class 
    }
}
