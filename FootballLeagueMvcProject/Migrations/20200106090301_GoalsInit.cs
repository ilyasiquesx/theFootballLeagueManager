using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class GoalsInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Fixtures");

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureId = table.Column<int>(nullable: true),
                    AuthorId = table.Column<int>(nullable: true),
                    AssistId = table.Column<int>(nullable: true),
                    ScoredAtMinute = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goal_Players_AssistId",
                        column: x => x.AssistId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goal_Players_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goal_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goal_AssistId",
                table: "Goal",
                column: "AssistId");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_AuthorId",
                table: "Goal",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_FixtureId",
                table: "Goal",
                column: "FixtureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
