using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApproval_History_FlowsheetApprover_flowsheetApprov~",
                table: "FlowsheetApproval_History");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApproval_History_Flowsheet_flowsheet_id",
                table: "FlowsheetApproval_History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlowsheetApproval_History",
                table: "FlowsheetApproval_History");

            migrationBuilder.RenameTable(
                name: "FlowsheetApproval_History",
                newName: "FlowsheetApprovalHistory");

            migrationBuilder.RenameIndex(
                name: "IX_FlowsheetApproval_History_flowsheetApprover_id",
                table: "FlowsheetApprovalHistory",
                newName: "IX_FlowsheetApprovalHistory_flowsheetApprover_id");

            migrationBuilder.RenameIndex(
                name: "IX_FlowsheetApproval_History_flowsheet_id",
                table: "FlowsheetApprovalHistory",
                newName: "IX_FlowsheetApprovalHistory_flowsheet_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlowsheetApprovalHistory",
                table: "FlowsheetApprovalHistory",
                column: "flowsheetApprovalHistory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprovalHistory_FlowsheetApprover_flowsheetApprove~",
                table: "FlowsheetApprovalHistory",
                column: "flowsheetApprover_id",
                principalTable: "FlowsheetApprover",
                principalColumn: "flowsheetApprover_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprovalHistory_Flowsheet_flowsheet_id",
                table: "FlowsheetApprovalHistory",
                column: "flowsheet_id",
                principalTable: "Flowsheet",
                principalColumn: "flowsheet_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprovalHistory_FlowsheetApprover_flowsheetApprove~",
                table: "FlowsheetApprovalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprovalHistory_Flowsheet_flowsheet_id",
                table: "FlowsheetApprovalHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlowsheetApprovalHistory",
                table: "FlowsheetApprovalHistory");

            migrationBuilder.RenameTable(
                name: "FlowsheetApprovalHistory",
                newName: "FlowsheetApproval_History");

            migrationBuilder.RenameIndex(
                name: "IX_FlowsheetApprovalHistory_flowsheetApprover_id",
                table: "FlowsheetApproval_History",
                newName: "IX_FlowsheetApproval_History_flowsheetApprover_id");

            migrationBuilder.RenameIndex(
                name: "IX_FlowsheetApprovalHistory_flowsheet_id",
                table: "FlowsheetApproval_History",
                newName: "IX_FlowsheetApproval_History_flowsheet_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlowsheetApproval_History",
                table: "FlowsheetApproval_History",
                column: "flowsheetApprovalHistory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApproval_History_FlowsheetApprover_flowsheetApprov~",
                table: "FlowsheetApproval_History",
                column: "flowsheetApprover_id",
                principalTable: "FlowsheetApprover",
                principalColumn: "flowsheetApprover_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApproval_History_Flowsheet_flowsheet_id",
                table: "FlowsheetApproval_History",
                column: "flowsheet_id",
                principalTable: "Flowsheet",
                principalColumn: "flowsheet_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
