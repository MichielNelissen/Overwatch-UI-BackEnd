using Microsoft.EntityFrameworkCore.Migrations;

namespace OverwatchAPI.Data.Migrations
{
    public partial class WidgetSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dashboards",
                columns: new[] { "Id", "Description", "ProjectId", "Name" },
                values: new object[,]
                {
                    { 1,  "Dit is een testboard", 1, "Dash1"}
                });

            migrationBuilder.InsertData(
                table: "Widgets",
                columns: new[] { "Id", "Color", "DashboardId", "Name" },
                values: new object[,]
                {
                    { 1, "Red1", 1, "Demo11" },
                    { 2, "Red2", 1, "Demo12" },
                    { 3, "Red3", 1, "Demo13" },
                    { 4, "Red4", 1, "Demo14" },
                    { 5, "Red5", 1, "Demo15" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Widgets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Widgets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Widgets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Widgets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Widgets",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
