using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Product_Image_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "71f519d4-1b0b-47ad-b9ed-f87de9d3a2cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8013966a-92c8-4f1e-874d-2f4f6cda50e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d519adf9-467f-42d9-9330-bef9b98e7bf5", "AQAAAAEAACcQAAAAEG+NT6W1vp07DmcRVjLN80eQ9IQKGCFmRll7K2t439E3qNJ0ZqQSmHd/ejP8s4CffQ==", "96114bed-5b0e-4822-b1f4-fab2f5214498" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6ffdb1b-0df8-44a4-b472-7449aa0bcf40", "AQAAAAEAACcQAAAAECBWXTFkPgzHOg19eAEAQReAO1calt9pwV3QiIlW16PWf46pxUE+GCdojIrHTQEcXQ==", "e1313b91-7535-4a63-9473-1b9cc16bfa5b" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 29, 14, 11, 8, 578, DateTimeKind.Local).AddTicks(6970));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4aff9873-421d-4a36-a663-657a9de3b783");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "925837a0-aee8-43ad-895f-a7e23c12cfdd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a33d050a-3a74-4dbc-bba7-ed890517cf27", "AQAAAAEAACcQAAAAEGLjL7GjSBYL/Bm3pUCiTOYHjCxN5GaXAW2yU609acaeg/Itegr0thQ9MPLdHsjqSw==", "965fe38d-73a5-4ddc-b531-a40e8f0864d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ba486e3-05bd-40e6-bc81-24a2c19eee0b", "AQAAAAEAACcQAAAAEN7C5WScmgeL/8lCF8cnKMjDsAB4nbQEjcb2HpaTrBNX9bLDUTQ7Bg8Sq7QbsLHZTw==", "79adf3ff-850b-48e3-bf03-6b0f6bdb4e7c" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 25, 14, 7, 31, 140, DateTimeKind.Local).AddTicks(4870));
        }
    }
}
