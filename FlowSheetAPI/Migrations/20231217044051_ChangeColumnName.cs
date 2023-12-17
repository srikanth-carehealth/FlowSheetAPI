using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ehr_user_name",
                table: "Patient");

            migrationBuilder.AddColumn<int>(
                name: "ehr_patient_id",
                table: "Patient",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ehr_patient_id",
                table: "Patient");

            migrationBuilder.AddColumn<string>(
                name: "ehr_user_name",
                table: "Patient",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
