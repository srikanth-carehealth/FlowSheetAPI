using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class EndocrinologyUpdatePatientColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "patient_id",
                table: "Endocrinology",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "patient_id",
                table: "Endocrinology",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
