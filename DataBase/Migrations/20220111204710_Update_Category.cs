using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class Update_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Product_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Product_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductCategory");

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
                        name: "FK_CategoryProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_ProductCategory_ProductCategorysId",
                        column: x => x.ProductCategorysId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductId",
                table: "CategoryProduct",
                column: "ProductId");
        }
    }
}
