using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryFK",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CategoryFK",
                table: "Product",
                newName: "IdCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryFK",
                table: "Product",
                newName: "IX_Product_IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_IdCategory",
                table: "Product",
                column: "IdCategory",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_IdCategory",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "Product",
                newName: "CategoryFK");

            migrationBuilder.RenameIndex(
                name: "IX_Product_IdCategory",
                table: "Product",
                newName: "IX_Product_CategoryFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryFK",
                table: "Product",
                column: "CategoryFK",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
