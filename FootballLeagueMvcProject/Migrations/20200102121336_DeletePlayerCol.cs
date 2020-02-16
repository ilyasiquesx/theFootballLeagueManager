using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class DeletePlayerCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Players_PlayerId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_PlayerId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Fixtures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Fixtures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_PlayerId",
                table: "Fixtures",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Players_PlayerId",
                table: "Fixtures",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
