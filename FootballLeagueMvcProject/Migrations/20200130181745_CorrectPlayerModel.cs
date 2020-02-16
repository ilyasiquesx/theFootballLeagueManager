using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class CorrectPlayerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtNumber",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtNumber",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Players");
        }
    }
}
