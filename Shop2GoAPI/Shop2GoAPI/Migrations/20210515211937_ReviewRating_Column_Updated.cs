using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class ReviewRating_Column_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ReviewRating",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ReviewRating",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "910b0951-d663-42db-9913-98838c0cff10");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5605d06c-0378-4154-a47f-7657c2cbaa49");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "310cfb1e-89a7-4f75-a6dc-c42ddf627c5f", "AQAAAAEAACcQAAAAEH+JDEiRFVqYQc9Q8j1lcPTZld6WSsgKfT5RwDR4KS5TPUwtLIL77VYfsqLxtEH5nQ==", "2fa66701-e8c1-4d65-a46f-1fe1f519c15e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56818b80-7107-4d9d-8013-fa81c589d911", "AQAAAAEAACcQAAAAEOLNBtxKZCdNAmTgcPTbMvR2HwQYJTGLkV86Ic65fFWYPHe/srr0/vjeTNuyneH8pA==", "8cb6d401-214a-4b29-9937-458621034bb3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 14, 17, 16, 6, 413, DateTimeKind.Local).AddTicks(7945));
        }
    }
}
