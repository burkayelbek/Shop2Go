using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class User_Favorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3524a38a-c5fd-427e-a78d-a04bbe605556");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "67a55d61-97b5-40bf-8619-49c9a47358ea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40be60f3-c545-4ff8-854c-8c6b359e4a38", "AQAAAAEAACcQAAAAEDBkQfNI9fJuF8hIzFdm865xlyHW8Riqd8IBRXx5sluS+/ScfyNODRL+96OnOe9gpg==", "311bb548-c5fe-464a-8c8f-ea8836857021" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 3, 19, 21, 13, 56, 394, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserID",
                table: "Favorites",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

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
                column: "PublishedDate",
                value: new DateTime(2021, 3, 17, 15, 3, 59, 469, DateTimeKind.Local).AddTicks(4892));
        }
    }
}
