using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelinkStodyGroupMasterSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "StudyGroupSubjects",
                newName: "MasterSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroupSubjects_MasterSubjects_MasterSubjectId",
                table: "StudyGroupSubjects",
                column: "MasterSubjectId",
                principalTable: "MasterSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroupSubjects_MasterSubjects_MasterSubjectId",
                table: "StudyGroupSubjects");

            migrationBuilder.RenameColumn(
                name: "MasterSubjectId",
                table: "StudyGroupSubjects",
                newName: "SubjectId");
        }
    }
}
