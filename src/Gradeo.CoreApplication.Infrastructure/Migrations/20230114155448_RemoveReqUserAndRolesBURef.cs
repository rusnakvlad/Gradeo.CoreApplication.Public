using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReqUserAndRolesBURef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_BusinessUnits_BusinessUnitId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_BusinessUnits_BusinessUnitId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUnitId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUnitId",
                table: "Roles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_BusinessUnits_BusinessUnitId",
                table: "Roles",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");

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
                name: "FK_Roles_BusinessUnits_BusinessUnitId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_BusinessUnits_BusinessUnitId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUnitId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUnitId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_BusinessUnits_BusinessUnitId",
                table: "Roles",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BusinessUnits_BusinessUnitId",
                table: "Users",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
