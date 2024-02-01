using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCompositeKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherStudyGroups",
                table: "TeacherStudyGroups");

            migrationBuilder.DropIndex(
                name: "IX_TeacherStudyGroups_StudyGroupId_TeacherProfileId",
                table: "TeacherStudyGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyGroupSubjects",
                table: "StudyGroupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroupSubjects_SubjectId_StudyGroupId",
                table: "StudyGroupSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentStudyGroups",
                table: "StudentStudyGroups");

            migrationBuilder.DropIndex(
                name: "IX_StudentStudyGroups_StudyGroupId_StudentProfileId",
                table: "StudentStudyGroups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeacherStudyGroups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudyGroupSubjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentStudyGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherStudyGroups",
                table: "TeacherStudyGroups",
                columns: new[] { "StudyGroupId", "TeacherProfileId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyGroupSubjects",
                table: "StudyGroupSubjects",
                columns: new[] { "SubjectId", "StudyGroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentStudyGroups",
                table: "StudentStudyGroups",
                columns: new[] { "StudyGroupId", "StudentProfileId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherStudyGroups",
                table: "TeacherStudyGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyGroupSubjects",
                table: "StudyGroupSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentStudyGroups",
                table: "StudentStudyGroups");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeacherStudyGroups",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudyGroupSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentStudyGroups",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherStudyGroups",
                table: "TeacherStudyGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyGroupSubjects",
                table: "StudyGroupSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentStudyGroups",
                table: "StudentStudyGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudyGroups_StudyGroupId_TeacherProfileId",
                table: "TeacherStudyGroups",
                columns: new[] { "StudyGroupId", "TeacherProfileId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupSubjects_SubjectId_StudyGroupId",
                table: "StudyGroupSubjects",
                columns: new[] { "SubjectId", "StudyGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudyGroups_StudyGroupId_StudentProfileId",
                table: "StudentStudyGroups",
                columns: new[] { "StudyGroupId", "StudentProfileId" });
        }
    }
}
