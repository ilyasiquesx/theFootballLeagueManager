using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class AddMigrationInit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Championships_ChampionshipId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ChampionshipId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ChampionshipId",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "ChampionshipTeam",
                columns: table => new
                {
                    ChampionshipId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionshipTeam", x => new { x.ChampionshipId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_ChampionshipTeam_Championships_ChampionshipId",
                        column: x => x.ChampionshipId,
                        principalTable: "Championships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChampionshipTeam_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChampionshipTeam_TeamId",
                table: "ChampionshipTeam",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChampionshipTeam");

            migrationBuilder.AddColumn<int>(
                name: "ChampionshipId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ChampionshipId",
                table: "Teams",
                column: "ChampionshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Championships_ChampionshipId",
                table: "Teams",
                column: "ChampionshipId",
                principalTable: "Championships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
