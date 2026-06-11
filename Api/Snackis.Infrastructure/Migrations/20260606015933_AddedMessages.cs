using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnackisMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenderUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnackisMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnackisMessages_AspNetUsers_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SnackisMessages_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnackisMessages_ReceiverUserId",
                table: "SnackisMessages",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SnackisMessages_SenderUserId",
                table: "SnackisMessages",
                column: "SenderUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnackisMessages");
        }
    }
}
