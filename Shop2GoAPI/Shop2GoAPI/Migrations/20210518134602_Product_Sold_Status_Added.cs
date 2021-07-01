using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Product_Sold_Status_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isSold",
                table: "Products",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSold",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e8e12591-fa21-43c3-8737-592cbbbf0959");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "87120c9a-1165-47fa-a842-b928a57f9b27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "955884be-a991-4e8c-9b3a-71f6d5a8b3bc", "AQAAAAEAACcQAAAAEDXD+CmO+OO/7tvNAsr5yhyHnaA7sZtAdaGrJR3G+A1ojRG4PCC9dWYaQeBSnsmhUg==", "69e70f70-7077-48ed-a49e-74fccabf5a03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8861c08c-2ab7-4955-8e72-3754e90aca1f", "AQAAAAEAACcQAAAAEBIvo/PGkdivKACeryjVicWjSHA1G0d60qWOfKCLhLjD1XQJBM35wKu2LsxwUOpODA==", "a5388e05-ea78-4647-9e2f-85d5f56c0b32" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 16, 0, 19, 36, 779, DateTimeKind.Local).AddTicks(6983));
        }
    }
}
