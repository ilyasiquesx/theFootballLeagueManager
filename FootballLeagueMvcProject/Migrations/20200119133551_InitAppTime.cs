using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class InitAppTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Fixtures_FixtureId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Fixtures");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointedTime",
                table: "Fixtures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fixtures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Fixtures_FixtureId",
                table: "Goals",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Fixtures_FixtureId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "AppointedTime",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Fixtures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Fixtures_FixtureId",
                table: "Goals",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
