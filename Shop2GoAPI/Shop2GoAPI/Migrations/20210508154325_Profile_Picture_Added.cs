using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Profile_Picture_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3ce46bde-9545-43ca-96fd-d43ec1f2a00b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4df3d50d-0e53-4bba-8734-e523c1cf7899");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce4b7b8b-ece0-463e-af7e-f12f1a553b9d", "AQAAAAEAACcQAAAAEDgEw1qn/WiAPyflgrOcD1/KsvrKTT10C3PIzyX4FpwmD55AEPdJ0lW0NE8jQ18CEA==", "f6fde3c5-eb78-45d6-8d58-a4349685e858" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1536eb61-6bc5-48fc-9b97-1ea29dcf8d33", "AQAAAAEAACcQAAAAEJdeCH9ezM6QLTf2Co+94EMIqB3PO3W13DEqW3aJtwDoxb4H1Dr5Eg13KSi3PUu4bQ==", "ed7fd666-2b0e-43e6-aa7c-96a32d52c484" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 8, 18, 43, 25, 102, DateTimeKind.Local).AddTicks(3917));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

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
        }
    }
}
