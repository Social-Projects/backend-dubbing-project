using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Migrations
{
    public partial class BaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Performances");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Audios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Performances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Audios",
                nullable: false,
                defaultValue: "");
        }
    }
}
