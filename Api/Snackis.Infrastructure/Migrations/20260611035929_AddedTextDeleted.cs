using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTextDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTextDeleted",
                table: "SnackisPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTextDeleted",
                table: "SnackisPosts");
        }
    }
}
