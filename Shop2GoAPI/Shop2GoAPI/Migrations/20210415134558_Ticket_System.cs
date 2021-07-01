using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Ticket_System : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    FixedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_UserID",
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

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserID",
                table: "Ticket",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "08c529b3-a711-4fd6-80d9-3fd09d40959f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8016e532-48a4-44cb-9a02-947ce62bb03e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "664b2dfd-eae1-4025-a1bd-cb6ff10694ad", "AQAAAAEAACcQAAAAEC/ZSoyqeKALz+HExf1O4AiiNDvK4gQ2Vw5nlJqC4FSvV6O6I8WdtroLz5Be0fHluw==", "c9dbf510-b1e3-43cc-a75f-0839f4691968" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52da2494-bd44-4582-b2e8-a442e984d793", "AQAAAAEAACcQAAAAEGkHobH+hzY8tVl8vowMEGv17JaYhTnSdq+eMCTarY0P4ah4wB3dTcws4pIZHLzLFw==", "b60f7a80-a433-4e11-b632-1dab8d2942ad" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 1, 15, 38, 20, 93, DateTimeKind.Local).AddTicks(3825));
        }
    }
}
