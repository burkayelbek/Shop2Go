using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Reviews_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewRating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(nullable: false),
                    SenderFullName = table.Column<string>(nullable: false),
                    RecieverId = table.Column<string>(nullable: false),
                    RecieverFullName = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StarRating = table.Column<float>(nullable: false),
                    SentTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewRating_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "310cfb1e-89a7-4f75-a6dc-c42ddf627c5f", "burkay.elbek@gmail.com", "BURKAY.ELBEK@GMAIL.COM", "AQAAAAEAACcQAAAAEH+JDEiRFVqYQc9Q8j1lcPTZld6WSsgKfT5RwDR4KS5TPUwtLIL77VYfsqLxtEH5nQ==", "2fa66701-e8c1-4d65-a46f-1fe1f519c15e", "Burkay", "Elbek" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "56818b80-7107-4d9d-8013-fa81c589d911", "auspiece.gamer@gmail.com", "AUSPIECE.GAMER@GMAIL.COM", "AQAAAAEAACcQAAAAEOLNBtxKZCdNAmTgcPTbMvR2HwQYJTGLkV86Ic65fFWYPHe/srr0/vjeTNuyneH8pA==", "8cb6d401-214a-4b29-9937-458621034bb3", "AdminName", "AdminSurname" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 14, 17, 16, 6, 413, DateTimeKind.Local).AddTicks(7945));

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRating_SenderId",
                table: "ReviewRating",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewRating");

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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "55fed1b1-f2bd-4e95-84a4-ac60d81276fe", "test@gmail.com", "TEST@GMAIL.COM", "AQAAAAEAACcQAAAAEJM8KbiOGQkx352muMbl12sBlSlmD9VHoUmkbrwCI5vFgUP3kTujvzQ3u5iXRYYwbg==", "d708bd65-ce9e-4ed6-a562-c664c2257a43", "testName", "testSurname" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "c4e8e78f-fd71-4453-a41f-ab39ff09779b", "testadmin@gmail.com", "TESTADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEOormrvJZejrmgD+sQKmFNiiiFqGtURcCVpplv1VMIFrXYeznBH1jbp+smfxY+C+RA==", "5c30347c-18bd-4610-a831-6124edbe9098", "testAdminName", "testAdminSurname" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(2021, 5, 11, 23, 46, 17, 876, DateTimeKind.Local).AddTicks(4499));
        }
    }
}
