using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "106365ea-d8b3-480f-bfb1-d9d672e268e5", null, "Administrator", "ADMINISTRATOR" },
                    { "80677512-f094-129c-caf3-e3f287a268d3", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "526375a2-e6a1-4d0a-aec3-e9a673fa68b4", 0, "e981de67-92ee-4759-91cb-e914d0f7ad6f", new DateTime(2023, 8, 26, 1, 52, 59, 535, DateTimeKind.Local).AddTicks(659), new DateTime(1984, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "lm-admin@test.net", true, "Isabella", "Reyes", false, null, "LM-ADMIN@TEST.NET", "LM-ADMIN@TEST.NET", "AQAAAAIAAYagAAAAEEDlKEHKWRcbFUkVAZqf0IcjXseTpOywCjd68pXjlJS6FXWVIHE8ZVzZH7iTEdQjOA==", null, false, "5280aed0-c0ce-4277-bf90-6d2d0f45a8f1", null, false, "lm-admin@test.net" },
                    { "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1", 0, "4811e443-acf4-4f89-8509-a0a30a7070d8", new DateTime(2023, 8, 26, 1, 52, 59, 606, DateTimeKind.Local).AddTicks(1747), new DateTime(1953, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "tobias@test.net", true, "Tobias", "Whale", false, null, "TOBIAS@TEST.NET", "TOBIAS@TEST.NET", "AQAAAAIAAYagAAAAEBdpWumzDp6ktCyXx8mTsFfKipSPH1eWED3WMNxInIMa6UA6v0htCuI+WRR3Mw/L4g==", null, false, "5f0de5bd-a1ae-4e96-9a10-431bcfc03d94", null, false, "tobias@test.net" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "106365ea-d8b3-480f-bfb1-d9d672e268e5", "526375a2-e6a1-4d0a-aec3-e9a673fa68b4" },
                    { "80677512-f094-129c-caf3-e3f287a268d3", "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "106365ea-d8b3-480f-bfb1-d9d672e268e5", "526375a2-e6a1-4d0a-aec3-e9a673fa68b4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "80677512-f094-129c-caf3-e3f287a268d3", "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "106365ea-d8b3-480f-bfb1-d9d672e268e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80677512-f094-129c-caf3-e3f287a268d3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "526375a2-e6a1-4d0a-aec3-e9a673fa68b4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab7375d3-e6b3-4f0a-ae93-e9a673fa32c1");
        }
    }
}
