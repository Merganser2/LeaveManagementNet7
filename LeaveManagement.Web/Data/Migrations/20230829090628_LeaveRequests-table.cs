using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRequeststable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // changed from Cascade; would rather have trouble deleting than delete too much
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "526375a2-e6a1-4d0a-aec3-e9a673fa68b4",
                columns: new[] { "ConcurrencyStamp", "DateJoined", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94498582-750e-42b2-96e4-4fe678813e57", new DateTime(2023, 8, 29, 2, 6, 28, 6, DateTimeKind.Local).AddTicks(1660), "AQAAAAIAAYagAAAAEEfBnHmkc/n7qz256/zKBVD7Tt7uBLXdJKmJLrEVYS+JyysMY9YnvG7CejihBgmwng==", "96ea9ba0-84ee-4d7a-8935-5f83d8f97fce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1",
                columns: new[] { "ConcurrencyStamp", "DateJoined", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2f96b1b-8f92-496c-9660-0d0428f1abc9", new DateTime(2023, 8, 29, 2, 6, 28, 87, DateTimeKind.Local).AddTicks(3882), "AQAAAAIAAYagAAAAEBAKDbPfi2C9xQ98rLeet4Up0lGtQmBPJCV4dFpiHYSA+IUgtAWvc06WQsmkkdgebg==", "19d037ad-2e24-4687-b0e2-596899cd841d" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

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
    }
}
