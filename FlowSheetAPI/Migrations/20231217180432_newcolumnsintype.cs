using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    /// <inheritdoc />
    public partial class newcolumnsintype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "SpecialityType",
                newName: "speciality_name");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "SpecialityType",
                newName: "speciality_code");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "SpecialityConditionType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "speciality_condition_code",
                table: "SpecialityConditionType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "LabItemSpeciality",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "LabItem",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "lab_item_code",
                table: "LabItem",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabItemSpeciality_lab_item_id",
                table: "LabItemSpeciality",
                column: "lab_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabItemSpeciality_LabItem_lab_item_id",
                table: "LabItemSpeciality",
                column: "lab_item_id",
                principalTable: "LabItem",
                principalColumn: "lab_item_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabItemSpeciality_LabItem_lab_item_id",
                table: "LabItemSpeciality");

            migrationBuilder.DropIndex(
                name: "IX_LabItemSpeciality_lab_item_id",
                table: "LabItemSpeciality");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "SpecialityConditionType");

            migrationBuilder.DropColumn(
                name: "speciality_condition_code",
                table: "SpecialityConditionType");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "LabItemSpeciality");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "LabItem");

            migrationBuilder.DropColumn(
                name: "lab_item_code",
                table: "LabItem");

            migrationBuilder.RenameColumn(
                name: "speciality_name",
                table: "SpecialityType",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "speciality_code",
                table: "SpecialityType",
                newName: "code");
        }
    }
}
