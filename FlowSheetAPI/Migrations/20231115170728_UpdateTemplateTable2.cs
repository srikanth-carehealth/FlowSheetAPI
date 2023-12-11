using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTemplateTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowsheetTemplate",
                columns: table => new
                {
                    flowsheetTemplate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    column_name = table.Column<string>(type: "text", nullable: false),
                    specialityType_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    row_version = table.Column<byte[]>(type: "bytea", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_FlowsheetTemplate_specialityType_id",
                table: "FlowsheetTemplate",
                column: "specialityType_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowsheetTemplate");
        }
    }
}
