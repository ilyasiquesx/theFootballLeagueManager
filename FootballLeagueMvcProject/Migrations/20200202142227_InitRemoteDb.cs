using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class InitRemoteDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Fixtures_FixtureId",
                table: "Cards");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Fixtures_FixtureId",
                table: "Cards",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Fixtures_FixtureId",
                table: "Cards");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Fixtures_FixtureId",
                table: "Cards",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
