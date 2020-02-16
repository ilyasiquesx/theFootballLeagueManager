using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class InitCardItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoredAtMinute",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "AtMinute",
                table: "Goals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtMinute",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "ScoredAtMinute",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
