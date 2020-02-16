using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeagueMvcProject.Migrations
{
    public partial class initdborles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6a174437-55e6-4115-a885-9f554f293b7b");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bf9492ce-1094-46ce-adda-6565e0e9a127", "17cae9ad-553e-44a0-812b-ad4d02db40fc", "Admin", null });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee57185e-83b7-4fcb-92fa-5a117efaff03", "1aa4a71c-b907-4575-93c4-1e68ab6500c7", "Moderator", null });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3aae3e6-c7f1-4fda-ab06-5bb78a5dd10e", "140514ce-5d88-45f1-8254-26574d68ebb8", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a3aae3e6-c7f1-4fda-ab06-5bb78a5dd10e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "bf9492ce-1094-46ce-adda-6565e0e9a127");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ee57185e-83b7-4fcb-92fa-5a117efaff03");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a174437-55e6-4115-a885-9f554f293b7b", "9df420a7-55de-40fb-bd45-37b8b6630800", "Admin", null });
        }
    }
}
