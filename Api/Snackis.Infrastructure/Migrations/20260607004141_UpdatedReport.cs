using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SnackisReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Solved",
                table: "SnackisReports",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SnackisReports");

            migrationBuilder.DropColumn(
                name: "Solved",
                table: "SnackisReports");
        }
    }
}
