using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class addnewtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover");

            migrationBuilder.DropTable(
                name: "FlowsheetType");

            migrationBuilder.AddColumn<string>(
                name: "client_id",
                table: "SpecialityType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "SpecialityType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_id",
                table: "SpecialityConditionType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "SpecialityConditionType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_id",
                table: "FlowsheetTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "FlowsheetTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "speciality_condition_type_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "specialityType_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_id",
                table: "FlowsheetApprover",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "FlowsheetApprover",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LabItem",
                columns: table => new
                {
                    lab_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lab_item_name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabItem", x => x.lab_item_id);
                });

            migrationBuilder.CreateTable(
                name: "LabItemSpeciality",
                columns: table => new
                {
                    lab_item_speciality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lab_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<string>(type: "text", nullable: false),
                    client_name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    speciality_contition_type_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabItemSpeciality", x => x.lab_item_speciality_id);
                    table.ForeignKey(
                        name: "FK_LabItemSpeciality_SpecialityConditionType_speciality_contit~",
                        column: x => x.speciality_contition_type_id,
                        principalTable: "SpecialityConditionType",
                        principalColumn: "speciality_condition_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabItemSpeciality_SpecialityType_specialityType_id",
                        column: x => x.specialityType_id,
                        principalTable: "SpecialityType",
                        principalColumn: "specialityType_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabItemSpeciality_speciality_contition_type_id",
                table: "LabItemSpeciality",
                column: "speciality_contition_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_LabItemSpeciality_specialityType_id",
                table: "LabItemSpeciality",
                column: "specialityType_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetApprover",
                column: "speciality_condition_type_id",
                principalTable: "SpecialityConditionType",
                principalColumn: "speciality_condition_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover",
                column: "specialityType_id",
                principalTable: "SpecialityType",
                principalColumn: "specialityType_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetApprover");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover");

            migrationBuilder.DropTable(
                name: "LabItem");

            migrationBuilder.DropTable(
                name: "LabItemSpeciality");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "SpecialityType");

            migrationBuilder.DropColumn(
                name: "client_name",
                table: "SpecialityType");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "SpecialityConditionType");

            migrationBuilder.DropColumn(
                name: "client_name",
                table: "SpecialityConditionType");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "FlowsheetTemplate");

            migrationBuilder.DropColumn(
                name: "client_name",
                table: "FlowsheetTemplate");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "FlowsheetApprover");

            migrationBuilder.DropColumn(
                name: "client_name",
                table: "FlowsheetApprover");

            migrationBuilder.AlterColumn<Guid>(
                name: "speciality_condition_type_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "specialityType_id",
                table: "FlowsheetApprover",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "FlowsheetType",
                columns: table => new
                {
                    flowsheetType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetType", x => x.flowsheetType_id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityConditionType_speciality_condit~",
                table: "FlowsheetApprover",
                column: "speciality_condition_type_id",
                principalTable: "SpecialityConditionType",
                principalColumn: "speciality_condition_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                table: "FlowsheetApprover",
                column: "specialityType_id",
                principalTable: "SpecialityType",
                principalColumn: "specialityType_id");
        }
    }
}
