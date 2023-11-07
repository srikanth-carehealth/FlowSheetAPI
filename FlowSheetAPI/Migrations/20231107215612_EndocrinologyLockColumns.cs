using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class EndocrinologyLockColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_locked",
                table: "Endocrinology",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "locked_by",
                table: "Endocrinology",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "locked_date",
                table: "Endocrinology",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_locked",
                table: "Endocrinology");

            migrationBuilder.DropColumn(
                name: "locked_by",
                table: "Endocrinology");

            migrationBuilder.DropColumn(
                name: "locked_date",
                table: "Endocrinology");
        }
    }
}
