using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class AddMigrationBoolStatusFixtur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fixtures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fixtures");
        }
    }
}
