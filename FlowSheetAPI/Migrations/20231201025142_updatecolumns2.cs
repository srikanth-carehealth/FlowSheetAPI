using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowsheetApprovalHistory");

            migrationBuilder.DropTable(
                name: "FlowsheetHistory");

            migrationBuilder.DropTable(
                name: "FlowsheetTemplate");

            migrationBuilder.DropTable(
                name: "FlowsheetType");

            migrationBuilder.DropTable(
                name: "FlowsheetApprover");

            migrationBuilder.DropTable(
                name: "Flowsheet");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "SpecialityType");

            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntity");

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ehr_user_name = table.Column<string>(type: "text", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.doctor_id);
                });

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

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    patient_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ehr_user_name = table.Column<string>(type: "text", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.patient_id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialityType",
                columns: table => new
                {
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    total_approval_count = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityType", x => x.specialityType_id);
                });

            migrationBuilder.CreateTable(
                name: "Flowsheet",
                columns: table => new
                {
                    flowsheet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    flowsheet_note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowsheet", x => x.flowsheet_id);
                    table.ForeignKey(
                        name: "FK_Flowsheet_Doctor_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flowsheet_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flowsheet_SpecialityType_specialityType_id",
                        column: x => x.specialityType_id,
                        principalTable: "SpecialityType",
                        principalColumn: "specialityType_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowsheetApprover",
                columns: table => new
                {
                    flowsheetApprover_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    designation = table.Column<string>(type: "text", nullable: false),
                    fax = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    initial = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    telephone = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "FlowsheetTemplate",
                columns: table => new
                {
                    flowsheetTemplate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    column_name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetTemplate", x => x.flowsheetTemplate_id);
                    table.ForeignKey(
                        name: "FK_FlowsheetTemplate_SpecialityType_specialityType_id",
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
                    flowsheet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    doctor_id = table.Column<string>(type: "text", nullable: false),
                    flowsheet_note = table.Column<string>(type: "text", nullable: false),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    locked_by = table.Column<string>(type: "text", nullable: true),
                    locked_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    patient_id = table.Column<string>(type: "text", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "FlowsheetApprovalHistory",
                columns: table => new
                {
                    flowsheetApprovalHistory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flowsheet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flowsheetApprover_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsheetApprovalHistory", x => x.flowsheetApprovalHistory_id);
                    table.ForeignKey(
                        name: "FK_FlowsheetApprovalHistory_FlowsheetApprover_flowsheetApprove~",
                        column: x => x.flowsheetApprover_id,
                        principalTable: "FlowsheetApprover",
                        principalColumn: "flowsheetApprover_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowsheetApprovalHistory_Flowsheet_flowsheet_id",
                        column: x => x.flowsheet_id,
                        principalTable: "Flowsheet",
                        principalColumn: "flowsheet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flowsheet_doctor_id",
                table: "Flowsheet",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flowsheet_patient_id",
                table: "Flowsheet",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flowsheet_specialityType_id",
                table: "Flowsheet",
                column: "specialityType_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetApprovalHistory_flowsheet_id",
                table: "FlowsheetApprovalHistory",
                column: "flowsheet_id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetApprovalHistory_flowsheetApprover_id",
                table: "FlowsheetApprovalHistory",
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

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetTemplate_specialityType_id",
                table: "FlowsheetTemplate",
                column: "specialityType_id");
        }
    }
}
