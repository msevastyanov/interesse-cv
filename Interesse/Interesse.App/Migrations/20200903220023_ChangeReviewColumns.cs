using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class ChangeReviewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Review");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Review",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Review",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Review");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Review",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
