using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class DB_Seeds_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "08c529b3-a711-4fd6-80d9-3fd09d40959f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8016e532-48a4-44cb-9a02-947ce62bb03e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "664b2dfd-eae1-4025-a1bd-cb6ff10694ad", "AQAAAAEAACcQAAAAEC/ZSoyqeKALz+HExf1O4AiiNDvK4gQ2Vw5nlJqC4FSvV6O6I8WdtroLz5Be0fHluw==", "c9dbf510-b1e3-43cc-a75f-0839f4691968" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "FirstName", "LastName" },
                values: new object[] { "2", 0, "52da2494-bd44-4582-b2e8-a442e984d793", "User", "testadmin@gmail.com", false, false, null, "TESTADMIN@GMAIL.COM", "TESTADMIN", "AQAAAAEAACcQAAAAEGkHobH+hzY8tVl8vowMEGv17JaYhTnSdq+eMCTarY0P4ah4wB3dTcws4pIZHLzLFw==", null, false, "b60f7a80-a433-4e11-b632-1dab8d2942ad", false, "testadmin", "testAdminName", "testAdminSurname" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 1, 15, 38, 20, 93, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3524a38a-c5fd-427e-a78d-a04bbe605556");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "67a55d61-97b5-40bf-8619-49c9a47358ea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40be60f3-c545-4ff8-854c-8c6b359e4a38", "AQAAAAEAACcQAAAAEDBkQfNI9fJuF8hIzFdm865xlyHW8Riqd8IBRXx5sluS+/ScfyNODRL+96OnOe9gpg==", "311bb548-c5fe-464a-8c8f-ea8836857021" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 3, 19, 21, 13, 56, 394, DateTimeKind.Local).AddTicks(5090));
        }
    }
}
