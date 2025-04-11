using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagmentSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27", null, "Administrator", "ADMINISTRATOR" },
                    { "4a154e49-6b8c-4b8e-851c-3ed673b19eec", null, "Supervisor", "SUPERVISOR" },
                    { "4cc9ef1b-224d-4718-8119-54039cbd8fe0", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d1aefdfd-eeb8-4998-a417-3b5d5989f244", 0, "862da8c8-9c28-4696-95e3-3be822fea568", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEGfsb7iqYFGkwWo4fA1f/S1DJG1R9EadZUC/04mWLgpCHZUwb+zl4xncPJiaIIkvmA==", null, false, "06ceabec-4b37-4338-aa6c-5d9db4b97d5c", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27", "d1aefdfd-eeb8-4998-a417-3b5d5989f244" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a154e49-6b8c-4b8e-851c-3ed673b19eec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cc9ef1b-224d-4718-8119-54039cbd8fe0");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27", "d1aefdfd-eeb8-4998-a417-3b5d5989f244" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a4ea0c3-5a50-42a3-8be2-f5e671f3ef27");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1aefdfd-eeb8-4998-a417-3b5d5989f244");
        }
    }
}
