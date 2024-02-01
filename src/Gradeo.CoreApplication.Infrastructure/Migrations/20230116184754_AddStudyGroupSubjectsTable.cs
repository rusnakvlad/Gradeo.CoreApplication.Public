using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyGroupSubjectsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyGroupSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudyGroupId = table.Column<int>(type: "int", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroupSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroupSubjects_AssignedSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "AssignedSubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyGroupSubjects_BusinessUnits_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyGroupSubjects_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupSubjects_BusinessUnitId",
                table: "StudyGroupSubjects",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupSubjects_IsDeleted",
                table: "StudyGroupSubjects",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupSubjects_StudyGroupId",
                table: "StudyGroupSubjects",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupSubjects_SubjectId_StudyGroupId",
                table: "StudyGroupSubjects",
                columns: new[] { "SubjectId", "StudyGroupId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyGroupSubjects");
        }
    }
}
