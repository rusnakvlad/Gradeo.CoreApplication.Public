using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherStudyGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherStudyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyGroupId = table.Column<int>(type: "int", nullable: false),
                    TeacherProfileId = table.Column<int>(type: "int", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherStudyGroups_BusinessUnits_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherStudyGroups_StudyGroups_TeacherProfileId",
                        column: x => x.TeacherProfileId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherStudyGroups_TeacherProfiles_TeacherProfileId",
                        column: x => x.TeacherProfileId,
                        principalTable: "TeacherProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudyGroups_BusinessUnitId",
                table: "TeacherStudyGroups",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudyGroups_StudyGroupId_TeacherProfileId",
                table: "TeacherStudyGroups",
                columns: new[] { "StudyGroupId", "TeacherProfileId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudyGroups_TeacherProfileId",
                table: "TeacherStudyGroups",
                column: "TeacherProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherStudyGroups");
        }
    }
}
