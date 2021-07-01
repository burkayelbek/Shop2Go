using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Conversation_Column_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentTime",
                table: "Conversation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d8be0aed-70e0-4d84-819f-cc6a409d13ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d3ec591b-ab03-4f72-9068-4b94c511aa9e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55fed1b1-f2bd-4e95-84a4-ac60d81276fe", "AQAAAAEAACcQAAAAEJM8KbiOGQkx352muMbl12sBlSlmD9VHoUmkbrwCI5vFgUP3kTujvzQ3u5iXRYYwbg==", "d708bd65-ce9e-4ed6-a562-c664c2257a43" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4e8e78f-fd71-4453-a41f-ab39ff09779b", "AQAAAAEAACcQAAAAEOormrvJZejrmgD+sQKmFNiiiFqGtURcCVpplv1VMIFrXYeznBH1jbp+smfxY+C+RA==", "5c30347c-18bd-4610-a831-6124edbe9098" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 11, 23, 46, 17, 876, DateTimeKind.Local).AddTicks(4499));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentTime",
                table: "Conversation");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "75dd5def-b71d-4231-a9d2-4ed5f5f12f67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e41091ac-860d-4351-9de6-80aef7bc84bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "635ed2d3-d99a-4feb-b492-d26153db25f8", "AQAAAAEAACcQAAAAEEGuvUNVykaAb6pOk2zMQUzuf3GVj5HJ0mhGC7r7czUOQ3PIfHZKhwcGfJfb/2wtnw==", "b4870bec-1501-4842-a46e-e7694a7a6811" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5936048f-331f-48a9-8da2-48c13c9a6cf0", "AQAAAAEAACcQAAAAEJ/bGRwW6I9b6c2ggM7aeNoTnU+TPgt0LTSBB2J0AMqniYca+XWSycQW+vwF7OnAYA==", "47619846-ce44-4367-b6b1-a661be6e293c" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 11, 21, 26, 41, 185, DateTimeKind.Local).AddTicks(4913));
        }
    }
}
