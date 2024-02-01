using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPermisssions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionDescriptors",
                columns: new[] { "PermissionId", "CreatedBy", "IsDeleted", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 0, null, false, null, "CanViewSchools" },
                    { 1, null, false, null, "CanCreateSchools" },
                    { 2, null, false, null, "CanEditSchools" },
                    { 3, null, false, null, "CanDeleteSchools" },
                    { 100, null, false, null, "CanViewUsers" },
                    { 101, null, false, null, "CanCreateUsers" },
                    { 102, null, false, null, "CanEditUsers" },
                    { 103, null, false, null, "CanDeleteUsers" },
                    { 200, null, false, null, "CanViewMasterSubjects" },
                    { 201, null, false, null, "CanCreateMasterSubjects" },
                    { 202, null, false, null, "CanEditMasterSubjects" },
                    { 203, null, false, null, "CanDeleteMasterSubjects" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 203);
        }
    }
}
