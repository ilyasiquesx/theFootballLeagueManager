using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class InitNavProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Championships_ChampionshipId",
                table: "Fixtures");

            migrationBuilder.AlterColumn<int>(
                name: "ChampionshipId",
                table: "Fixtures",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Championships_ChampionshipId",
                table: "Fixtures",
                column: "ChampionshipId",
                principalTable: "Championships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Championships_ChampionshipId",
                table: "Fixtures");

            migrationBuilder.AlterColumn<int>(
                name: "ChampionshipId",
                table: "Fixtures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Championships_ChampionshipId",
                table: "Fixtures",
                column: "ChampionshipId",
                principalTable: "Championships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
