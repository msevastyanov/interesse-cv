using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class RenamePageCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Page_PageCategories_PageCategoryId",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageCategories",
                table: "PageCategories");

            migrationBuilder.RenameTable(
                name: "PageCategories",
                newName: "PageCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageCategory",
                table: "PageCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_PageCategory_PageCategoryId",
                table: "Page",
                column: "PageCategoryId",
                principalTable: "PageCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Page_PageCategory_PageCategoryId",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageCategory",
                table: "PageCategory");

            migrationBuilder.RenameTable(
                name: "PageCategory",
                newName: "PageCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageCategories",
                table: "PageCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_PageCategories_PageCategoryId",
                table: "Page",
                column: "PageCategoryId",
                principalTable: "PageCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
