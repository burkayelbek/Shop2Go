using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Category_Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "31db3010-ef50-4b6e-a102-cf614b215cc8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8f6fab06-f41b-4b8e-a7af-f381b3ef67d6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "802d6244-70e9-4751-ae8c-fae91b869142", "AQAAAAEAACcQAAAAEGGp3ujsW3Bvyj5OBY3XZcy3mUwaPjnypooTmzwEeit1FTe+BDGEqMz368W25Y0zzg==", "7c7c4506-cee7-49cd-b57b-37ea2d4d6aab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fee661c8-a1bb-4427-9e7c-23addb90e905", "AQAAAAEAACcQAAAAEEY7JuecGro8onH0ZnUllKQasWxdOqVgWNsuolwK47LM800EDLiIj7Kq3pl6YRAP5g==", "b54ad9e6-7bdd-49d6-a746-e2f0a5743bdd" });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Electronics");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 22, 15, 50, 19, 255, DateTimeKind.Local).AddTicks(2504));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Test Category");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 19, 1, 33, 11, 617, DateTimeKind.Local).AddTicks(7834));
        }
    }
}
