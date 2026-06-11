using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnackisReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    PostCommentId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnackisReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnackisReport_SnackisPostComments_PostCommentId",
                        column: x => x.PostCommentId,
                        principalTable: "SnackisPostComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SnackisReport_SnackisPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "SnackisPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnackisReport_PostCommentId",
                table: "SnackisReport",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_SnackisReport_PostId",
                table: "SnackisReport",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnackisReport");
        }
    }
}
