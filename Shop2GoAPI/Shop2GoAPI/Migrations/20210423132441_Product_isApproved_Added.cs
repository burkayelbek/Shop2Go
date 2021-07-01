using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Product_isApproved_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isApproved",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3430d1d5-c13e-4cf9-9b4a-dfb21da709cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3c3cd267-f061-4cfa-a14f-ddd451b6c496");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "091aa747-e6e5-4956-9dd4-e942fc1d08a1", "AQAAAAEAACcQAAAAEC0jrMahG42les27++jpoyBJXWd1XwWytafKT7kJAwvg9KMm68KUluZ8Ttr++FXNNA==", "92dca599-fd63-451a-b75c-97eed8ed9f40" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c01c003-7e00-40c7-8d30-47124ab825fb", "AQAAAAEAACcQAAAAEAXMAoyJNDRPzvT2DuQMo7g23ECFjAgGGmGAn8ms0OJYZU7toHgX73/MCSn5H2dPTw==", "4d1a2d8f-36c9-450f-9839-4f7ed54f7b50" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PublishedDate", "isApproved" },
                values: new object[] { new DateTime(2021, 4, 23, 16, 24, 41, 121, DateTimeKind.Local).AddTicks(3013), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "1d94244d-e8ee-4f20-a7ed-b70d614ef51d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f624e5b2-bd20-40ef-b7e2-9823cc16d8a4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6f88581-4cbb-4945-8b7f-de081ce5f395", "AQAAAAEAACcQAAAAEPwkMYorMbduN2BwjmQ+ZEuFwMdUF9pGs2cxT9q0R3VzeWAs4kxQjQhQnwKL1K2Kyw==", "35e970b6-ceb2-4272-bad5-fa7c49e20665" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a858dd8-db69-45a4-9247-e418ff924ac1", "AQAAAAEAACcQAAAAEOgGPuHbQkhDMZjE8DcrmPojiDZHgutCYnQnZpgJTO6Kr9mSRKNkmHCmyMtKtgLNmQ==", "6ff1c4be-8f10-4d08-ad76-d62bf7568bb1" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 19, 2, 24, 50, 597, DateTimeKind.Local).AddTicks(7426));
        }
    }
}
