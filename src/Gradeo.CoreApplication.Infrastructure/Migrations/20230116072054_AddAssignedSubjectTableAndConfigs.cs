using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedSubjectTableAndConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "MasterSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssignedSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterSubjectId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                name: "IX_MasterSubjects_BusinessUnitId",
                table: "MasterSubjects",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_BusinessUnitId",
                table: "AssignedSubjects",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_MasterSubjectId",
                table: "AssignedSubjects",
                column: "MasterSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterSubjects_BusinessUnits_BusinessUnitId",
                table: "MasterSubjects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterSubjects_BusinessUnits_BusinessUnitId",
                table: "MasterSubjects");

            migrationBuilder.DropTable(
                name: "AssignedSubjects");

            migrationBuilder.DropIndex(
                name: "IX_MasterSubjects_BusinessUnitId",
                table: "MasterSubjects");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "MasterSubjects");
        }
    }
}
