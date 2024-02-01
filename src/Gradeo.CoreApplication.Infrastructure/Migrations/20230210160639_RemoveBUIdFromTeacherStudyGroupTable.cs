using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBUIdFromTeacherStudyGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherStudyGroups_BusinessUnits_BusinessUnitId",
                table: "TeacherStudyGroups");

            migrationBuilder.DropIndex(
                name: "IX_TeacherStudyGroups_BusinessUnitId",
                table: "TeacherStudyGroups");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "TeacherStudyGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "TeacherStudyGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudyGroups_BusinessUnitId",
                table: "TeacherStudyGroups",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherStudyGroups_BusinessUnits_BusinessUnitId",
                table: "TeacherStudyGroups",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }
    }
}
