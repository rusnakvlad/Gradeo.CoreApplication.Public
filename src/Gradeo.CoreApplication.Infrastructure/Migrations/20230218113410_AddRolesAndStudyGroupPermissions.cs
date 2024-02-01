using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndStudyGroupPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionDescriptors",
                columns: new[] { "PermissionId", "CreatedBy", "IsDeleted", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 700, null, false, null, "CanViewRoles" },
                    { 701, null, false, null, "CanCreateRoles" },
                    { 702, null, false, null, "CanEditRoles" },
                    { 703, null, false, null, "CanDeleteRoles" },
                    { 800, null, false, null, "CanViewStudyGroups" },
                    { 801, null, false, null, "CanCreateStudyGroups" },
                    { 802, null, false, null, "CanEditStudyGroups" },
                    { 803, null, false, null, "CanDeleteStudyGroups" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "PermissionDescriptors",
                keyColumn: "PermissionId",
                keyValue: 803);
        }
    }
}
