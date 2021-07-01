using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop2GoAPI.Migrations
{
    public partial class Conversation_DbSet_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<string>(nullable: false),
                    SenderId = table.Column<string>(nullable: false),
                    SenderFullName = table.Column<string>(nullable: false),
                    RecieverId = table.Column<string>(nullable: false),
                    RecieverFullName = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_AspNetUsers_SenderId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_SenderId",
                table: "Conversation",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversation");

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
    }
}
