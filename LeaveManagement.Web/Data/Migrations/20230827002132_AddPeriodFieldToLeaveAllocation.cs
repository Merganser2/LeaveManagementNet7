using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodFieldToLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "526375a2-e6a1-4d0a-aec3-e9a673fa68b4",
                columns: new[] { "ConcurrencyStamp", "DateJoined", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7635bce0-ef43-4e78-b97d-c3fa91ec7ae5", new DateTime(2023, 8, 26, 17, 21, 31, 929, DateTimeKind.Local).AddTicks(9689), "AQAAAAIAAYagAAAAEMxeYomHkzcQ+losMe6aGsu4owsuUTDzP2l+fcH6yWuubzZSysSrjLy8fNvjqIl0jA==", "3cee153a-6644-43e1-84fc-0ab6d1bf3e39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1",
                columns: new[] { "ConcurrencyStamp", "DateJoined", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05cf6a27-1a08-43f3-a7ac-59ebef4c2cb5", new DateTime(2023, 8, 26, 17, 21, 31, 997, DateTimeKind.Local).AddTicks(2450), "AQAAAAIAAYagAAAAEDrFk3EwovRc2PI/84POhP92lp/HMKukctUZHUcBvTS9CjRzK8GwG1NDbchfE4nMfg==", "8b3121af-2197-4b37-b687-482020536e85" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "526375a2-e6a1-4d0a-aec3-e9a673fa68b4",
                columns: new[] { "ConcurrencyStamp", "DateJoined", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e981de67-92ee-4759-91cb-e914d0f7ad6f", new DateTime(2023, 8, 26, 1, 52, 59, 535, DateTimeKind.Local).AddTicks(659), "AQAAAAIAAYagAAAAEEDlKEHKWRcbFUkVAZqf0IcjXseTpOywCjd68pXjlJS6FXWVIHE8ZVzZH7iTEdQjOA==", "5280aed0-c0ce-4277-bf90-6d2d0f45a8f1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1",
                columns: new[] { "ConcurrencyStamp", "DateJoined", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4811e443-acf4-4f89-8509-a0a30a7070d8", new DateTime(2023, 8, 26, 1, 52, 59, 606, DateTimeKind.Local).AddTicks(1747), "AQAAAAIAAYagAAAAEBdpWumzDp6ktCyXx8mTsFfKipSPH1eWED3WMNxInIMa6UA6v0htCuI+WRR3Mw/L4g==", "5f0de5bd-a1ae-4e96-9a10-431bcfc03d94" });
        }
    }
}
