using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdOfUserTableAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles",
                column: "UserId");
        }
    }
}
