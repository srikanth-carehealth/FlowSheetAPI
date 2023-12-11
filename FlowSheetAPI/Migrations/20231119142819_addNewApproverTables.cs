using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNewApproverTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_locked",
                table: "Flowsheet");

            migrationBuilder.DropColumn(
                name: "locked_by",
                table: "Flowsheet");

            migrationBuilder.DropColumn(
                name: "locked_date",
                table: "Flowsheet");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "SpecialityType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "total_approval_count",
                table: "SpecialityType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FlowsheetApprover",
                columns: table => new
                {
                    flowsheetApprover_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    initial = table.Column<string>(type: "text", nullable: false),
                    designation = table.Column<string>(type: "text", nullable: false),
                    telephone = table.Column<string>(type: "text", nullable: true),
                    fax = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetApprover", x => x.flowsheetApprover_id);
                    table.ForeignKey(
                        name: "FK_FlowsheetApprover_SpecialityType_specialityType_id",
                        column: x => x.specialityType_id,
                        principalTable: "SpecialityType",
                        principalColumn: "specialityType_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowsheetHistory",
                columns: table => new
                {
                    flowsheetHistory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<string>(type: "text", nullable: false),
                    doctor_id = table.Column<string>(type: "text", nullable: false),
                    flowsheet_note = table.Column<string>(type: "text", nullable: false),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    locked_by = table.Column<string>(type: "text", nullable: true),
                    locked_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flowsheet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetHistory", x => x.flowsheetHistory_id);
                    table.ForeignKey(
                        name: "FK_FlowsheetHistory_Flowsheet_flowsheet_id",
                        column: x => x.flowsheet_id,
                        principalTable: "Flowsheet",
                        principalColumn: "flowsheet_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowsheetHistory_SpecialityType_specialityType_id",
                        column: x => x.specialityType_id,
                        principalTable: "SpecialityType",
                        principalColumn: "specialityType_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowsheetType",
                columns: table => new
                {
                    flowsheetType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetType", x => x.flowsheetType_id);
                });

            migrationBuilder.CreateTable(
                name: "FlowsheetApproval_History",
                columns: table => new
                {
                    flowsheetApprovalHistory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flowsheet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flowsheetApprover_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetApproval_History", x => x.flowsheetApprovalHistory_id);
                    table.ForeignKey(
                        name: "FK_FlowsheetApproval_History_FlowsheetApprover_flowsheetApprov~",
                        column: x => x.flowsheetApprover_id,
                        principalTable: "FlowsheetApprover",
                        principalColumn: "flowsheetApprover_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowsheetApproval_History_Flowsheet_flowsheet_id",
                        column: x => x.flowsheet_id,
                        principalTable: "Flowsheet",
                        principalColumn: "flowsheet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetApproval_History_flowsheet_id",
                table: "FlowsheetApproval_History",
                column: "flowsheet_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetApproval_History_flowsheetApprover_id",
                table: "FlowsheetApproval_History",
                column: "flowsheetApprover_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetApprover_specialityType_id",
                table: "FlowsheetApprover",
                column: "specialityType_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetHistory_flowsheet_id",
                table: "FlowsheetHistory",
                column: "flowsheet_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetHistory_specialityType_id",
                table: "FlowsheetHistory",
                column: "specialityType_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowsheetApproval_History");

            migrationBuilder.DropTable(
                name: "FlowsheetHistory");

            migrationBuilder.DropTable(
                name: "FlowsheetType");

            migrationBuilder.DropTable(
                name: "FlowsheetApprover");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "SpecialityType");

            migrationBuilder.DropColumn(
                name: "total_approval_count",
                table: "SpecialityType");

            migrationBuilder.AddColumn<bool>(
                name: "is_locked",
                table: "Flowsheet",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "locked_by",
                table: "Flowsheet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "locked_date",
                table: "Flowsheet",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
