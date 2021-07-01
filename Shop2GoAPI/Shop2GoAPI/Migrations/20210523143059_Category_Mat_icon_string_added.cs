using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Category_Mat_icon_string_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatIconName",
                table: "Category",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b1e607e5-a2dc-47a1-a9d5-8b176c10f6a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c418f5d8-ebac-43a6-bf49-42b961da9934");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15c04c57-6eb8-4aa7-bef7-5522d48ba16c", "AQAAAAEAACcQAAAAEPa9k2WWV5tO4osK+tvgcSz71zlOx9oGBxH/aWuU6nGJ3AOLPun9GXUAzG+7LQP1dA==", "7bc76f99-b15c-4cec-ad8a-e9f6a7191476" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15a7070b-1854-4abf-9c6c-da11a7168773", "AQAAAAEAACcQAAAAEASMgLmMChiXX2QFjZXa8iXz3OgUSapMjQVQuiDpgLOjuHayOvd0ZN66SpFUYCN3hw==", "6b3db443-15e6-4421-a9e8-cced8d762361" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 23, 17, 30, 58, 622, DateTimeKind.Local).AddTicks(105));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatIconName",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

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
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 22, 15, 50, 19, 255, DateTimeKind.Local).AddTicks(2504));
        }
    }
}
