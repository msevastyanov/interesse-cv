using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class RenameLuckToSuccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLuck",
                table: "Application");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "Application",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Application");

            migrationBuilder.AddColumn<bool>(
                name: "IsLuck",
                table: "Application",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
