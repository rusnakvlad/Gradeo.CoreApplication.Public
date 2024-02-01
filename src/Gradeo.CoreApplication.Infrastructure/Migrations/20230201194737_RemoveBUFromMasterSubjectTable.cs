using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBUFromMasterSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterSubjects_BusinessUnits_BusinessUnitId",
                table: "MasterSubjects");

            migrationBuilder.DropIndex(
                name: "IX_MasterSubjects_BusinessUnitId",
                table: "MasterSubjects");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "MasterSubjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "MasterSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterSubjects_BusinessUnitId",
                table: "MasterSubjects",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterSubjects_BusinessUnits_BusinessUnitId",
                table: "MasterSubjects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }
    }
}
