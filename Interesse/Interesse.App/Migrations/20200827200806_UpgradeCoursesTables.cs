using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class UpgradeCoursesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseCategoryId",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Course",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHome",
                table: "Course",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CourseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ChangedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    MetaTitle = table.Column<string>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ShowOnHome = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseCategoryId",
                table: "Course",
                column: "CourseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseCategories_CourseCategoryId",
                table: "Course",
                column: "CourseCategoryId",
                principalTable: "CourseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseCategories_CourseCategoryId",
                table: "Course");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropIndex(
                name: "IX_Course_CourseCategoryId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseCategoryId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ShowOnHome",
                table: "Course");
        }
    }
}
