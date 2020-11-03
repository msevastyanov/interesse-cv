using Microsoft.EntityFrameworkCore.Migrations;

namespace Interesse.App.Migrations
{
    public partial class AddCategoryIdToPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Page_PageCategory_PageCategoryId",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_PageCategoryId",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "PageCategoryId",
                table: "Page");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Page",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_CategoryId",
                table: "Page",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_PageCategory_CategoryId",
                table: "Page",
                column: "CategoryId",
                principalTable: "PageCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Page_PageCategory_CategoryId",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_CategoryId",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Page");

            migrationBuilder.AddColumn<int>(
                name: "PageCategoryId",
                table: "Page",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_PageCategoryId",
                table: "Page",
                column: "PageCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_PageCategory_PageCategoryId",
                table: "Page",
                column: "PageCategoryId",
                principalTable: "PageCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
