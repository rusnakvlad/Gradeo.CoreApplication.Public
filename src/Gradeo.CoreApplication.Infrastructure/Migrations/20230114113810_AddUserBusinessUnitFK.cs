using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBusinessUnitFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BusinessUnitId",
                table: "Users",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BusinessUnits_BusinessUnitId",
                table: "Users",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BusinessUnits_BusinessUnitId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BusinessUnitId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "Users");
        }
    }
}
