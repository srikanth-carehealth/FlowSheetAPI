using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Patient",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "FlowsheetType",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "FlowsheetTemplate",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "FlowsheetHistory",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "FlowsheetApprover",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "FlowsheetApprovalHistory",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Flowsheet",
                newName: "row_version");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "Doctor",
                newName: "row_version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "Patient",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "FlowsheetType",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "FlowsheetTemplate",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "FlowsheetHistory",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "FlowsheetApprover",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "FlowsheetApprovalHistory",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "Flowsheet",
                newName: "RowVersion");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "Doctor",
                newName: "RowVersion");
        }
    }
}
