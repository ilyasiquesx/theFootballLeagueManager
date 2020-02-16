using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class RemarkTechDefId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechDedeatedTeamId",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "TechDefeatedTeamId",
                table: "Fixtures",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechDefeatedTeamId",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "TechDedeatedTeamId",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
