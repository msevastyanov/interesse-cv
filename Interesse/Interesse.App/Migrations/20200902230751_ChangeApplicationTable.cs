using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class ChangeApplicationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "CourseTime",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Application");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Application",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Application",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Application");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Application",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseTime",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseType",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Application",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
