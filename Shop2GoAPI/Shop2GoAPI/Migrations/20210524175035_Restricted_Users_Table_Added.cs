using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Restricted_Users_Table_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestrictedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: false),
                    RestrictedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestrictedUsers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "69668a61-ad01-4125-8b66-d446e3e7f680");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d728ebcd-bc11-4827-ab86-c4f265437353");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d493b71e-169f-4160-b262-1911ed1197f0", "AQAAAAEAACcQAAAAEDj/FnJJJrHXqGu6JShy2fAApPMclFDCItgcZrYbuXxDhy+yS/sddYZ18VD0noB/CQ==", "1c796cde-a496-4781-b515-56bea595281b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3336b445-e195-4fcc-bc92-8d3de715e1c4", "AQAAAAEAACcQAAAAEAUondOSXX7Oq+h+sWWctI/OEOOFZtjnXFfYs6B7cCawa/aCtfHqdXk3P4lGsx5gnQ==", "dabccd18-7f0b-4738-98df-305c4aa8a585" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 24, 20, 50, 35, 403, DateTimeKind.Local).AddTicks(6406));

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedUsers_UserID",
                table: "RestrictedUsers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestrictedUsers");

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
    }
}
