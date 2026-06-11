using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReportsDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SnackisReport_SnackisPostComments_PostCommentId",
                table: "SnackisReport");

            migrationBuilder.DropForeignKey(
                name: "FK_SnackisReport_SnackisPosts_PostId",
                table: "SnackisReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SnackisReport",
                table: "SnackisReport");

            migrationBuilder.RenameTable(
                name: "SnackisReport",
                newName: "SnackisReports");

            migrationBuilder.RenameIndex(
                name: "IX_SnackisReport_PostId",
                table: "SnackisReports",
                newName: "IX_SnackisReports_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_SnackisReport_PostCommentId",
                table: "SnackisReports",
                newName: "IX_SnackisReports_PostCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SnackisReports",
                table: "SnackisReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnackisReports_SnackisPostComments_PostCommentId",
                table: "SnackisReports",
                column: "PostCommentId",
                principalTable: "SnackisPostComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnackisReports_SnackisPosts_PostId",
                table: "SnackisReports",
                column: "PostId",
                principalTable: "SnackisPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SnackisReports_SnackisPostComments_PostCommentId",
                table: "SnackisReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SnackisReports_SnackisPosts_PostId",
                table: "SnackisReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SnackisReports",
                table: "SnackisReports");

            migrationBuilder.RenameTable(
                name: "SnackisReports",
                newName: "SnackisReport");

            migrationBuilder.RenameIndex(
                name: "IX_SnackisReports_PostId",
                table: "SnackisReport",
                newName: "IX_SnackisReport_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_SnackisReports_PostCommentId",
                table: "SnackisReport",
                newName: "IX_SnackisReport_PostCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SnackisReport",
                table: "SnackisReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnackisReport_SnackisPostComments_PostCommentId",
                table: "SnackisReport",
                column: "PostCommentId",
                principalTable: "SnackisPostComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnackisReport_SnackisPosts_PostId",
                table: "SnackisReport",
                column: "PostId",
                principalTable: "SnackisPosts",
                principalColumn: "Id");
        }
    }
}
