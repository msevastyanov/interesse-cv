using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class ChangeCourseImageColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "CourseCategories");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "PreviewImage",
                table: "CourseCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviewImage",
                table: "Course",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImage",
                table: "CourseCategories");

            migrationBuilder.DropColumn(
                name: "PreviewImage",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CourseCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
