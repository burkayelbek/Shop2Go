using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Ticket_System_Column_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "Ticket",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f713f8aa-ed3b-4b85-8122-6a920e782144");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ee444815-9195-4262-abc6-3cc41c9af3a5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80060b3c-6f3f-4f8e-a7e4-6e2a4b36e4e0", "AQAAAAEAACcQAAAAENdkaPNWEXrgrHtRSXGua1ICOcXgkwJco8nAnI/AxaPE08UOUQpvShcSFDfEUwFMHw==", "7918cf2e-1792-4476-a36d-38ac869a7fcf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16ed4404-14ba-432e-a846-a92d49d024d8", "AQAAAAEAACcQAAAAEM0VTr65yQVsO2ZCU674EUFBorTO764GGbBqdKsy7X+ggwZnET7Wq80cVfPlY8kJWg==", "89f04ea7-3109-443f-804c-0ae3cda94bfb" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 15, 19, 41, 58, 195, DateTimeKind.Local).AddTicks(5918));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "Ticket");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7d0921e8-6a95-45b9-bbb4-c6aed396a938");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "76160523-3a76-4ae1-bf60-7641584b2a39");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6dfaa98-f70e-4582-8a7a-12574abe23b8", "AQAAAAEAACcQAAAAEIxdFi/r11bVpHR4bRBO9lzI6q0cPu4vqjH//nre5ZgBaCIYy7tU3TqmQQXv1/L0QQ==", "785c66d9-6d21-433a-9be3-1c40fda43f3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8560c83-e1bd-467e-ad5e-942872a0b1c9", "AQAAAAEAACcQAAAAEH80I9Fvrg/epZdmg322neZ+c2eyv4M0MtQlTaZa/VM/WdAM/f8qSI7YImdZRei7vA==", "13aa573b-f5f5-49e1-be1a-cf7228c40f62" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 15, 16, 45, 57, 700, DateTimeKind.Local).AddTicks(6132));
        }
    }
}
