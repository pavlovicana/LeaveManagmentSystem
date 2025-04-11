using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1aefdfd-eeb8-4998-a417-3b5d5989f244",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01b40ef8-2ddd-4b2d-bf82-0c4103b63224", new DateOnly(1994, 5, 21), "Default", "Admin", "AQAAAAIAAYagAAAAEPNpGd5YvaSRguIo9WNAvOu65YzwP/rMmv7YIwAeP+0YT2Q+vdnfQd8rljKizm8gvA==", "7430a31a-1c81-4243-8a93-27ceb91e2e6f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1aefdfd-eeb8-4998-a417-3b5d5989f244",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "862da8c8-9c28-4696-95e3-3be822fea568", "AQAAAAIAAYagAAAAEGfsb7iqYFGkwWo4fA1f/S1DJG1R9EadZUC/04mWLgpCHZUwb+zl4xncPJiaIIkvmA==", "06ceabec-4b37-4338-aa6c-5d9db4b97d5c" });
        }
    }
}
