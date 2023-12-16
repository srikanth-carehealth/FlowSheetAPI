using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class addSpecialityConditionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "speciality_condition_type_id",
                table: "FlowsheetTemplate",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "speciality_condition_type_id",
                table: "FlowsheetHistory",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "speciality_condition_type_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "speciality_condition_type_id",
                table: "Flowsheet",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SpecialityConditionType",
                columns: table => new
                {
                    speciality_condition_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    condition_name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    speciality_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityConditionType", x => x.speciality_condition_type_id);
                    table.ForeignKey(
                        name: "FK_SpecialityConditionType_SpecialityType_speciality_id",
                        column: x => x.speciality_id,
                        principalTable: "SpecialityType",
                        principalColumn: "specialityType_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetTemplate_speciality_condition_type_id",
                table: "FlowsheetTemplate",
                column: "speciality_condition_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetHistory_speciality_condition_type_id",
                table: "FlowsheetHistory",
                column: "speciality_condition_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetApprover_speciality_condition_type_id",
                table: "FlowsheetApprover",
                column: "speciality_condition_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flowsheet_speciality_condition_type_id",
                table: "Flowsheet",
                column: "speciality_condition_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialityConditionType_speciality_id",
                table: "SpecialityConditionType",
                column: "speciality_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowsheet_SpecialityConditionType_speciality_condition_type~",
                table: "Flowsheet",
                column: "speciality_condition_type_id",
                principalTable: "SpecialityConditionType",
                principalColumn: "speciality_condition_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetApprover",
                column: "speciality_condition_type_id",
                principalTable: "SpecialityConditionType",
                principalColumn: "speciality_condition_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetHistory_SpecialityConditionType_speciality_conditi~",
                table: "FlowsheetHistory",
                column: "speciality_condition_type_id",
                principalTable: "SpecialityConditionType",
                principalColumn: "speciality_condition_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetTemplate_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetTemplate",
                column: "speciality_condition_type_id",
                principalTable: "SpecialityConditionType",
                principalColumn: "speciality_condition_type_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowsheet_SpecialityConditionType_speciality_condition_type~",
                table: "Flowsheet");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetHistory_SpecialityConditionType_speciality_conditi~",
                table: "FlowsheetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetTemplate_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetTemplate");

            migrationBuilder.DropTable(
                name: "SpecialityConditionType");

            migrationBuilder.DropIndex(
                name: "IX_FlowsheetTemplate_speciality_condition_type_id",
                table: "FlowsheetTemplate");

            migrationBuilder.DropIndex(
                name: "IX_FlowsheetHistory_speciality_condition_type_id",
                table: "FlowsheetHistory");

            migrationBuilder.DropIndex(
                name: "IX_FlowsheetApprover_speciality_condition_type_id",
                table: "FlowsheetApprover");

            migrationBuilder.DropIndex(
                name: "IX_Flowsheet_speciality_condition_type_id",
                table: "Flowsheet");

            migrationBuilder.DropColumn(
                name: "speciality_condition_type_id",
                table: "FlowsheetTemplate");

            migrationBuilder.DropColumn(
                name: "speciality_condition_type_id",
                table: "FlowsheetHistory");

            migrationBuilder.DropColumn(
                name: "speciality_condition_type_id",
                table: "FlowsheetApprover");

            migrationBuilder.DropColumn(
                name: "speciality_condition_type_id",
                table: "Flowsheet");
        }
    }
}
