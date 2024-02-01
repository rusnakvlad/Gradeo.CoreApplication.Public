using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBUFromStudStudyGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudyGroups_BusinessUnits_BusinessUnitId",
                table: "StudentStudyGroups");

            migrationBuilder.DropIndex(
                name: "IX_StudentStudyGroups_BusinessUnitId",
                table: "StudentStudyGroups");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "StudentStudyGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "StudentStudyGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudyGroups_BusinessUnitId",
                table: "StudentStudyGroups",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudyGroups_BusinessUnits_BusinessUnitId",
                table: "StudentStudyGroups",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }
    }
}
