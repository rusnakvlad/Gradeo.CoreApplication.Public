using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNotNeededBUInStudyGroupSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroupSubjects_BusinessUnits_BusinessUnitId",
                table: "StudyGroupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroupSubjects_BusinessUnitId",
                table: "StudyGroupSubjects");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "StudyGroupSubjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "StudyGroupSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupSubjects_BusinessUnitId",
                table: "StudyGroupSubjects",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroupSubjects_BusinessUnits_BusinessUnitId",
                table: "StudyGroupSubjects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }
    }
}
