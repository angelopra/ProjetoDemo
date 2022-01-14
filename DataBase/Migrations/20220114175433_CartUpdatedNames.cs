using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class CartUpdatedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Product_ProductId",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartItem",
                newName: "IdProduct");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItem",
                newName: "IdCart");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                newName: "IX_CartItem_IdProduct");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                newName: "IX_CartItem_IdCart");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_IdCart",
                table: "CartItem",
                column: "IdCart",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Product_IdProduct",
                table: "CartItem",
                column: "IdProduct",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_IdCart",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Product_IdProduct",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "CartItem",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "IdCart",
                table: "CartItem",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_IdProduct",
                table: "CartItem",
                newName: "IX_CartItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_IdCart",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Product_ProductId",
                table: "CartItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
