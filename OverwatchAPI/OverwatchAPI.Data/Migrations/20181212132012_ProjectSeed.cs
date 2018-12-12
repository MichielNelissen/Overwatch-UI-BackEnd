using Microsoft.EntityFrameworkCore.Migrations;

namespace OverwatchAPI.Data.Migrations
{
    public partial class ProjectSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[] { 1, "Overwatch", "an url" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
