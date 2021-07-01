using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Added_ReviewId_to_Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "BuyerUserName",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9cf24819-54e8-4aa7-9049-d258a1d2ac27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d4ee1a92-d716-4bb1-9f47-92050d883bb2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca37b919-f0ff-442c-beb0-02398729c84a", "AQAAAAEAACcQAAAAEHurO4RcKfLHM2v0gmz9TZ8pMSpkZV30y0JUbdmUKSB6PkRMlbJL2Q5qkmkfTVJNmg==", "1d2e3adb-df07-47d8-9653-b206deff1acd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83058214-056e-457c-8c41-70b92f6e9014", "AQAAAAEAACcQAAAAECI30HlBctxt6MFGOXtLLZiuGn/UQnpyfabfYmp6612Tja4okvv+8hXj0X2mPyxEnA==", "18e3ad90-7c6a-425d-8a3e-af65953f4bf7" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 19, 1, 33, 11, 617, DateTimeKind.Local).AddTicks(7834));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerUserName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7996e668-b2b6-4c45-b5b3-16c0edba0657");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6b52b570-47ba-4af8-b0d2-d407e8183897");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3959250-0220-464c-a7e9-c47e09fd0621", "AQAAAAEAACcQAAAAEM64Vr5SpMlwYJgTZTMLFqzFDhUmRVyNB0e/oFskiZbwNFt4xGauyJFx58xmmLMEcw==", "acd68e33-7fdb-4903-b5bf-57202c5e108d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26017055-dff7-4251-bfa7-769a14da5f82", "AQAAAAEAACcQAAAAEHhTVxzP+iMwQ31lT+vBE2qKfcUayzi1YjLnHJ4nwdwlKEJKlWSiHh+J8JP2kqdeNQ==", "b2fe11f4-39c5-448f-81a7-cd859d455562" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 18, 22, 18, 55, 159, DateTimeKind.Local).AddTicks(156));
        }
    }
}
