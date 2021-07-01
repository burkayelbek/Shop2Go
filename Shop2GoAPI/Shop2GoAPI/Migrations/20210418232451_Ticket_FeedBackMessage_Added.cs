using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Ticket_FeedBackMessage_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeedbackMessage",
                table: "Ticket",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackMessage",
                table: "Ticket");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a253b720-63a2-4811-b061-837ef758d3d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "14ca235d-73fc-4b53-8ad2-affebc443737");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22c4b25e-c0db-4f26-84ae-709836cb9604", "AQAAAAEAACcQAAAAEG+P1IqlqzCmKv5B2itFGHBff6PDXekcAg/Ps4Xp5aOAv9t2WZcQNWt9n+HCDrlCqA==", "b108a221-80cb-40f6-93ec-1915991d350e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bd6ee5c-65e9-42b2-b9d7-89dc16e990fd", "AQAAAAEAACcQAAAAECyukrTrvHQyPps0ec8V2g9P9PaSw1rR/VsmCum7vWRzIowtA7nGIdn2rgbFT+q+MA==", "3492db67-b40d-4c9c-b5d3-c916541a2ac7" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 18, 15, 47, 17, 251, DateTimeKind.Local).AddTicks(6992));
        }
    }
}
