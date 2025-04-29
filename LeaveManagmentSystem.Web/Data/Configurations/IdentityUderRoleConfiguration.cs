using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Web.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
            {
                RoleId = "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27",
                UserId = "d1aefdfd-eeb8-4998-a417-3b5d5989f244"
            });
        }
    }
}
