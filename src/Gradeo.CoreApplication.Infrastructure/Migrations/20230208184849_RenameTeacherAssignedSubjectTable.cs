using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTeacherAssignedSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignedSubjects_BusinessUnits_BusinessUnitId",
                table: "TeacherAssignedSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignedSubjects_TeacherProfiles_TeacherProfileId",
                table: "TeacherAssignedSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherAssignedSubjects",
                table: "TeacherAssignedSubjects");

            migrationBuilder.RenameTable(
                name: "TeacherAssignedSubjects",
                newName: "TeacherMasterSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherAssignedSubjects_BusinessUnitId",
                table: "TeacherMasterSubjects",
                newName: "IX_TeacherMasterSubjects_BusinessUnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherMasterSubjects",
                table: "TeacherMasterSubjects",
                columns: new[] { "TeacherProfileId", "AssignedSubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherMasterSubjects_BusinessUnits_BusinessUnitId",
                table: "TeacherMasterSubjects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherMasterSubjects_TeacherProfiles_TeacherProfileId",
                table: "TeacherMasterSubjects",
                column: "TeacherProfileId",
                principalTable: "TeacherProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherMasterSubjects_BusinessUnits_BusinessUnitId",
                table: "TeacherMasterSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherMasterSubjects_TeacherProfiles_TeacherProfileId",
                table: "TeacherMasterSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherMasterSubjects",
                table: "TeacherMasterSubjects");

            migrationBuilder.RenameTable(
                name: "TeacherMasterSubjects",
                newName: "TeacherAssignedSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherMasterSubjects_BusinessUnitId",
                table: "TeacherAssignedSubjects",
                newName: "IX_TeacherAssignedSubjects_BusinessUnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherAssignedSubjects",
                table: "TeacherAssignedSubjects",
                columns: new[] { "TeacherProfileId", "AssignedSubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignedSubjects_BusinessUnits_BusinessUnitId",
                table: "TeacherAssignedSubjects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignedSubjects_TeacherProfiles_TeacherProfileId",
                table: "TeacherAssignedSubjects",
                column: "TeacherProfileId",
                principalTable: "TeacherProfiles",
                principalColumn: "Id");
        }
    }
}
