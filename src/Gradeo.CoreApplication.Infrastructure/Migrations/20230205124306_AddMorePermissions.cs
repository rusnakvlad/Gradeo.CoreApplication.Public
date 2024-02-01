using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMorePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "PermissionDescriptors",
                columns: new[] { "PermissionId", "CreatedBy", "IsDeleted", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 300, null, false, null, "CanViewStudents" },
                    { 301, null, false, null, "CanCreateStudents" },
                    { 302, null, false, null, "CanEditStudents" },
                    { 303, null, false, null, "CanDeleteStudents" },
                    { 400, null, false, null, "CanViewTeachers" },
                    { 401, null, false, null, "CanCreateTeachers" },
                    { 402, null, false, null, "CanEditTeachers" },
                    { 403, null, false, null, "CanDeleteTeachers" },
                    { 500, null, false, null, "CanViewAdminData" },
                    { 501, null, false, null, "CanViewStatistics" },
                    { 600, null, false, null, "CanViewGrades" },
                    { 601, null, false, null, "CanCreateGrades" },
                    { 602, null, false, null, "CanEditGrades" },
                    { 603, null, false, null, "CanDeleteGrades" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 603);

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Roles");
        }
    }
}
