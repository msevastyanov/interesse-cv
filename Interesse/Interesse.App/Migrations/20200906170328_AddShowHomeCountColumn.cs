using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class AddShowHomeCountColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnHome",
                table: "PageCategory");

            migrationBuilder.AddColumn<int>(
                name: "ShowHomeCount",
                table: "PageCategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowHomeCount",
                table: "PageCategory");

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHome",
                table: "PageCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
