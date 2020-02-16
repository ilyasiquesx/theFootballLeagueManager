using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class AddMigrationOtherMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChampionshipId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChampionshipId",
                table: "Fixtures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Championship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Championship", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ChampionshipId",
                table: "Teams",
                column: "ChampionshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_ChampionshipId",
                table: "Fixtures",
                column: "ChampionshipId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Championship_ChampionshipId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Championship_ChampionshipId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Championship");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ChampionshipId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_ChampionshipId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "ChampionshipId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ChampionshipId",
                table: "Fixtures");
        }
    }
}
