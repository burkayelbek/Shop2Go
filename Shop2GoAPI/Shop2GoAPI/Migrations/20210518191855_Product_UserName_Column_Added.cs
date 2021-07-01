using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Product_UserName_Column_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Products",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7bc7cf4a-4392-45e4-bffb-921af2da90ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f6ba0706-61c6-4ed4-b19d-6d9471a413ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cce6a58a-2dc9-4a34-8de0-e32ff94e8925", "AQAAAAEAACcQAAAAEImateAZby6m9g39W00EAYlW7kcxJpsO8Ce1EsbBE19q7epSFVOiF5rcQK8LezcjyA==", "9ed6d333-11f9-47de-b7bc-1de7a4712aac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0602ec1a-0e77-40c1-9f99-e3a44e995498", "AQAAAAEAACcQAAAAEKMbPYycev+k8jnHCLpSjfJrt46ReB15ym2uxTrTUNER+w/lf8rKfo09LEIMKleu4Q==", "f7f25ac4-5345-4811-a509-a3b09cadcf4f" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 18, 16, 46, 1, 928, DateTimeKind.Local).AddTicks(4767));
        }
    }
}
