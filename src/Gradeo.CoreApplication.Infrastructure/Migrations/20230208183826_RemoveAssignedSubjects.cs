using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAssignedSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroupSubjects_AssignedSubjects_SubjectId",
                table: "StudyGroupSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignedSubjects_AssignedSubjects_AssignedSubjectId",
                table: "TeacherAssignedSubjects");

            migrationBuilder.DropTable(
                name: "AssignedSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignedSubjects_AssignedSubjectId",
                table: "TeacherAssignedSubjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignedSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    MasterSubjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_BusinessUnits_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_MasterSubjects_MasterSubjectId",
                        column: x => x.MasterSubjectId,
                        principalTable: "MasterSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignedSubjects_AssignedSubjectId",
                table: "TeacherAssignedSubjects",
                column: "AssignedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_BusinessUnitId",
                table: "AssignedSubjects",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_MasterSubjectId",
                table: "AssignedSubjects",
                column: "MasterSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroupSubjects_AssignedSubjects_SubjectId",
                table: "StudyGroupSubjects",
                column: "SubjectId",
                principalTable: "AssignedSubjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignedSubjects_AssignedSubjects_AssignedSubjectId",
                table: "TeacherAssignedSubjects",
                column: "AssignedSubjectId",
                principalTable: "AssignedSubjects",
                principalColumn: "Id");
        }
    }
}
