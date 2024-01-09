using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "FlowsheetTemplate",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "FlowsheetTemplate");
        }
    }
}
