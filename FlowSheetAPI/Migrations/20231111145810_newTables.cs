using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class newTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endocrinology");

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ehr_user_name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.doctor_id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    patient_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ehr_user_name = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
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
                    value = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
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
                    flowsheet_note = table.Column<string>(type: "text", nullable: false),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    locked_by = table.Column<string>(type: "text", nullable: true),
                    locked_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flowsheet");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "SpecialityType");

            migrationBuilder.CreateTable(
                name: "Endocrinology",
                columns: table => new
                {
                    EndocrinologyId = table.Column<Guid>(type: "uuid", nullable: false),
                    A1C = table.Column<float>(type: "real", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Dose = table.Column<string>(type: "text", nullable: true),
                    initial_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    locked_by = table.Column<string>(type: "text", nullable: true),
                    locked_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Medication = table.Column<string>(type: "text", nullable: true),
                    patient_id = table.Column<string>(type: "text", nullable: false),
                    Recommendation = table.Column<string>(type: "text", nullable: true),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endocrinology", x => x.EndocrinologyId);
                });
        }
    }
}
