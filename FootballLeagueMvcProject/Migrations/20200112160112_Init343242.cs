using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class Init343242 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Players_AssistId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Players_AuthorId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Fixtures_FixtureId",
                table: "Goal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_FixtureId",
                table: "Goals",
                newName: "IX_Goals_FixtureId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_AuthorId",
                table: "Goals",
                newName: "IX_Goals_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_AssistId",
                table: "Goals",
                newName: "IX_Goals_AssistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Players_AssistId",
                table: "Goals",
                column: "AssistId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Players_AuthorId",
                table: "Goals",
                column: "AuthorId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Fixtures_FixtureId",
                table: "Goals",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Players_AssistId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Players_AuthorId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Fixtures_FixtureId",
                table: "Goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_FixtureId",
                table: "Goal",
                newName: "IX_Goal_FixtureId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_AuthorId",
                table: "Goal",
                newName: "IX_Goal_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_AssistId",
                table: "Goal",
                newName: "IX_Goal_AssistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Players_AssistId",
                table: "Goal",
                column: "AssistId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Players_AuthorId",
                table: "Goal",
                column: "AuthorId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Fixtures_FixtureId",
                table: "Goal",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
