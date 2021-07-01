using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Alter_Identity_Data_Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "771e26f5-f6e3-4ee6-b457-d6c2c5f028de", "Admin", "Admin" },
                    { "2", "ad094353-906b-4b8e-94bd-a8fd3d5f52e8", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "FirstName", "LastName" },
                values: new object[] { "1", 0, "18b3b14e-4191-4c01-a3e2-f30ea3e39ed8", "User", "test@gmail.com", false, false, null, "TEST@GMAIL.COM", "TESTUSER", "AQAAAAEAACcQAAAAEPZ/2UlWsub3qrP15hOEfAdoX5K5z1ui51oUtkpKsktqg/SACKyRMV8rouBm6KG2Sw==", null, false, "57eebbc0-862f-43b6-b310-459b6c8da9a3", false, "testuser", "testName", "testSurname" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 3, 17, 14, 38, 5, 0, DateTimeKind.Local).AddTicks(2208));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 3, 6, 16, 27, 19, 838, DateTimeKind.Local).AddTicks(9908));
        }
    }
}
