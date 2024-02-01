using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTeacherStudyGroupRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherStudyGroups_StudyGroups_TeacherProfileId",
                table: "TeacherStudyGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherStudyGroups_StudyGroups_StudyGroupId",
                table: "TeacherStudyGroups",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherStudyGroups_StudyGroups_StudyGroupId",
                table: "TeacherStudyGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherStudyGroups_StudyGroups_TeacherProfileId",
                table: "TeacherStudyGroups",
                column: "TeacherProfileId",
                principalTable: "StudyGroups",
                principalColumn: "Id");
        }
    }
}
