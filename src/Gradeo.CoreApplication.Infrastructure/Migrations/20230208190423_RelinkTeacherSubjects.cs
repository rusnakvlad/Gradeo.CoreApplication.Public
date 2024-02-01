using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelinkTeacherSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedSubjectId",
                table: "TeacherMasterSubjects",
                newName: "MasterSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMasterSubjects_MasterSubjectId",
                table: "TeacherMasterSubjects",
                column: "MasterSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherMasterSubjects_MasterSubjects_MasterSubjectId",
                table: "TeacherMasterSubjects",
                column: "MasterSubjectId",
                principalTable: "MasterSubjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherMasterSubjects_MasterSubjects_MasterSubjectId",
                table: "TeacherMasterSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherMasterSubjects_MasterSubjectId",
                table: "TeacherMasterSubjects");

            migrationBuilder.RenameColumn(
                name: "MasterSubjectId",
                table: "TeacherMasterSubjects",
                newName: "AssignedSubjectId");
        }
    }
}
