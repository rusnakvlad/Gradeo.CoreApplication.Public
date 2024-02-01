using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDefaultGradeoUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, new Guid("888726f8-5476-4d55-acb6-98bd304e1033") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("888726f8-5476-4d55-acb6-98bd304e1033"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BusinessUnitId", "CreatedBy", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastName", "UserType" },
                values: new object[] { new Guid("a004cf46-45db-4aa4-92f9-8215ffce3f4f"), null, null, "gradeoadmin@gradeo.com", "Gradeo", false, null, "Admin", null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, new Guid("a004cf46-45db-4aa4-92f9-8215ffce3f4f"), null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, new Guid("a004cf46-45db-4aa4-92f9-8215ffce3f4f") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a004cf46-45db-4aa4-92f9-8215ffce3f4f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BusinessUnitId", "CreatedBy", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastName", "UserType" },
                values: new object[] { new Guid("888726f8-5476-4d55-acb6-98bd304e1033"), null, null, "gradeoadmin@gradeo.com", "Gradeo", false, null, "Admin", null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, new Guid("888726f8-5476-4d55-acb6-98bd304e1033"), null, null });
        }
    }
}
