using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class FlowSheetTemplateSchemachange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "column_id",
                table: "FlowsheetTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "column_sort_order",
                table: "FlowsheetTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "column_id",
                table: "FlowsheetTemplate");

            migrationBuilder.DropColumn(
                name: "column_sort_order",
                table: "FlowsheetTemplate");
        }
    }
}
