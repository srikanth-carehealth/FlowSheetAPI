using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class Approvers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover");

            migrationBuilder.AlterColumn<Guid>(
                name: "specialityType_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "flowsheetApprover_id",
                table: "Flowsheet",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flowsheet_flowsheetApprover_id",
                table: "Flowsheet",
                column: "flowsheetApprover_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowsheet_FlowsheetApprover_flowsheetApprover_id",
                table: "Flowsheet",
                column: "flowsheetApprover_id",
                principalTable: "FlowsheetApprover",
                principalColumn: "flowsheetApprover_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover",
                column: "specialityType_id",
                principalTable: "SpecialityType",
                principalColumn: "specialityType_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowsheet_FlowsheetApprover_flowsheetApprover_id",
                table: "Flowsheet");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover");

            migrationBuilder.DropIndex(
                name: "IX_Flowsheet_flowsheetApprover_id",
                table: "Flowsheet");

            migrationBuilder.DropColumn(
                name: "flowsheetApprover_id",
                table: "Flowsheet");

            migrationBuilder.AlterColumn<Guid>(
                name: "specialityType_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover",
                column: "specialityType_id",
                principalTable: "SpecialityType",
                principalColumn: "specialityType_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
