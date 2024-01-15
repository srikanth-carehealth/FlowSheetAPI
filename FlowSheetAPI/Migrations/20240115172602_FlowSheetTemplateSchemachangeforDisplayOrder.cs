using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class FlowSheetTemplateSchemachangeforDisplayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "column_sort_order",
                table: "FlowsheetTemplate");

            migrationBuilder.AddColumn<int>(
                name: "column_display_order",
                table: "FlowsheetTemplate",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "column_display_order",
                table: "FlowsheetTemplate");

            migrationBuilder.AddColumn<string>(
                name: "column_sort_order",
                table: "FlowsheetTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
