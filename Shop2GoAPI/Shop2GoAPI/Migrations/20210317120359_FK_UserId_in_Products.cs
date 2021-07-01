using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class FK_UserId_in_Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2f1cf9d5-4ede-41b6-9942-fad23d51c72e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4212f2b6-2a65-4220-b3e2-8a5eb1a747a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f704be7a-e2a3-4341-9d01-e25f964f4462", "AQAAAAEAACcQAAAAEOupUPxQ39gBU5Ngnr+3MYcwMGLyvofzZ1ZyCJwun6jmT7tD84FafIvP5eqaM17VXw==", "2d7d8e77-94dc-4695-b8bc-7eadbb1ccf20" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PublishedDate", "UserID" },
                values: new object[] { new DateTime(2021, 3, 17, 15, 3, 59, 469, DateTimeKind.Local).AddTicks(4892), "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserID",
                table: "Products",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserID",
                table: "Products",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "771e26f5-f6e3-4ee6-b457-d6c2c5f028de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ad094353-906b-4b8e-94bd-a8fd3d5f52e8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18b3b14e-4191-4c01-a3e2-f30ea3e39ed8", "AQAAAAEAACcQAAAAEPZ/2UlWsub3qrP15hOEfAdoX5K5z1ui51oUtkpKsktqg/SACKyRMV8rouBm6KG2Sw==", "57eebbc0-862f-43b6-b310-459b6c8da9a3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 3, 17, 14, 38, 5, 0, DateTimeKind.Local).AddTicks(2208));
        }
    }
}
