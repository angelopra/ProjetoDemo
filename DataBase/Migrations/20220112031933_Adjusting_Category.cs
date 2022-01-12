using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class Adjusting_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Product_ProductId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ProductId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    ProductCategorysId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.ProductCategorysId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Category_ProductCategorysId",
                        column: x => x.ProductCategorysId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductId",
                table: "CategoryProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ProductId",
                table: "Category",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Product_ProductId",
                table: "Category",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
