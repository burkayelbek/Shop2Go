using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Product_Rejected_Message_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectedMessage",
                table: "Products",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedMessage",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3430d1d5-c13e-4cf9-9b4a-dfb21da709cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3c3cd267-f061-4cfa-a14f-ddd451b6c496");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "091aa747-e6e5-4956-9dd4-e942fc1d08a1", "AQAAAAEAACcQAAAAEC0jrMahG42les27++jpoyBJXWd1XwWytafKT7kJAwvg9KMm68KUluZ8Ttr++FXNNA==", "92dca599-fd63-451a-b75c-97eed8ed9f40" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c01c003-7e00-40c7-8d30-47124ab825fb", "AQAAAAEAACcQAAAAEAXMAoyJNDRPzvT2DuQMo7g23ECFjAgGGmGAn8ms0OJYZU7toHgX73/MCSn5H2dPTw==", "4d1a2d8f-36c9-450f-9839-4f7ed54f7b50" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 4, 23, 16, 24, 41, 121, DateTimeKind.Local).AddTicks(3013));
        }
    }
}
