using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class AddPagesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "PageCategory");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "PageCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlignCenter",
                table: "Page",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Page",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "PageCategory");

            migrationBuilder.DropColumn(
                name: "IsAlignCenter",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Page");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "PageCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
