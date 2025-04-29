using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Web.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
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
        }
    }
}
