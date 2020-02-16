using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class ChampInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Championship_ChampionshipId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Championship_ChampionshipId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Championship",
                table: "Championship");

            migrationBuilder.RenameTable(
                name: "Championship",
                newName: "Championships");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Championships",
                table: "Championships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Championships_ChampionshipId",
                table: "Fixtures",
                column: "ChampionshipId",
                principalTable: "Championships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Championships_ChampionshipId",
                table: "Teams",
                column: "ChampionshipId",
                principalTable: "Championships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Championships_ChampionshipId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Championships_ChampionshipId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Championships",
                table: "Championships");

            migrationBuilder.RenameTable(
                name: "Championships",
                newName: "Championship");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Championship",
                table: "Championship",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Championship_ChampionshipId",
                table: "Fixtures",
                column: "ChampionshipId",
                principalTable: "Championship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Championship_ChampionshipId",
                table: "Teams",
                column: "ChampionshipId",
                principalTable: "Championship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
