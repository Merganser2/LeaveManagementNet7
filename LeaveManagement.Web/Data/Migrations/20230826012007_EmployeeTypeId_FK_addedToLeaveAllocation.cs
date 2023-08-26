using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeTypeId_FK_addedToLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeTypeId",
                table: "LeaveAllocations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeTypeId",
                table: "LeaveAllocations",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeTypeId",
                table: "LeaveAllocations",
                column: "EmployeeTypeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_EmployeeTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "LeaveAllocations");
        }
    }
}
