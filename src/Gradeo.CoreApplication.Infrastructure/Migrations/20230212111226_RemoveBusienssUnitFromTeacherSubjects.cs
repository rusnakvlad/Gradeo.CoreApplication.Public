using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBusienssUnitFromTeacherSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherMasterSubjects_BusinessUnits_BusinessUnitId",
                table: "TeacherMasterSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherMasterSubjects_BusinessUnitId",
                table: "TeacherMasterSubjects");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "TeacherMasterSubjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "TeacherMasterSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMasterSubjects_BusinessUnitId",
                table: "TeacherMasterSubjects",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherMasterSubjects_BusinessUnits_BusinessUnitId",
                table: "TeacherMasterSubjects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }
    }
}
