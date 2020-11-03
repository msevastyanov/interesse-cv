using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class ChangeFewPageAndCategoryColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLanguage",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "ShowOnHome",
                table: "Page");

            migrationBuilder.AddColumn<int>(
                name: "RenderType",
                table: "PageCategory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RenderType",
                table: "PageCategory");

            migrationBuilder.AddColumn<int>(
                name: "CourseLanguage",
                table: "Page",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHome",
                table: "Page",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
