using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Ticket_TitleColumn_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Ticket",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Ticket");

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
    }
}
