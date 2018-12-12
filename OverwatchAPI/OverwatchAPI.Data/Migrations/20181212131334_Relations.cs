using Microsoft.EntityFrameworkCore.Migrations;

namespace OverwatchAPI.Data.Migrations
{
    public partial class Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Projects_ProjectId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Widget_Dashboards_DashboardId",
                table: "Widget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Widget",
                table: "Widget");

            migrationBuilder.RenameTable(
                name: "Widget",
                newName: "Widgets");

            migrationBuilder.RenameIndex(
                name: "IX_Widget_DashboardId",
                table: "Widgets",
                newName: "IX_Widgets_DashboardId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Dashboards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DashboardId",
                table: "Widgets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Widgets",
                table: "Widgets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Projects_ProjectId",
                table: "Dashboards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Widgets_Dashboards_DashboardId",
                table: "Widgets",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Projects_ProjectId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Widgets_Dashboards_DashboardId",
                table: "Widgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Widgets",
                table: "Widgets");

            migrationBuilder.RenameTable(
                name: "Widgets",
                newName: "Widget");

            migrationBuilder.RenameIndex(
                name: "IX_Widgets_DashboardId",
                table: "Widget",
                newName: "IX_Widget_DashboardId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Dashboards",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DashboardId",
                table: "Widget",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Widget",
                table: "Widget",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Projects_ProjectId",
                table: "Dashboards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widget_Dashboards_DashboardId",
                table: "Widget",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
