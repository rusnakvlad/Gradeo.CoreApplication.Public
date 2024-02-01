using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherAssignedSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherAssignedSubjects",
                columns: table => new
                {
                    TeacherProfileId = table.Column<int>(type: "int", nullable: false),
                    AssignedSubjectId = table.Column<int>(type: "int", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssignedSubjects", x => new { x.TeacherProfileId, x.AssignedSubjectId });
                    table.ForeignKey(
                        name: "FK_TeacherAssignedSubjects_AssignedSubjects_AssignedSubjectId",
                        column: x => x.AssignedSubjectId,
                        principalTable: "AssignedSubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherAssignedSubjects_BusinessUnits_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherAssignedSubjects_TeacherProfiles_TeacherProfileId",
                        column: x => x.TeacherProfileId,
                        principalTable: "TeacherProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignedSubjects_AssignedSubjectId",
                table: "TeacherAssignedSubjects",
                column: "AssignedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignedSubjects_BusinessUnitId",
                table: "TeacherAssignedSubjects",
                column: "BusinessUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherAssignedSubjects");
        }
    }
}
