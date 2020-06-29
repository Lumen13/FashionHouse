using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionHouse.Data.EF.Migrations
{
    public partial class piecesIntoEntityRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributeValuesProducts");

            migrationBuilder.CreateTable(
                name: "AttributesValuesProductEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ProductAttributeId = table.Column<int>(nullable: false),
                    ProductAttributeValueId = table.Column<int>(nullable: false),
                    Pieces = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributesValuesProductEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributesValuesProductEntities_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttributesValuesProductEntities_ProductAttributeValues_ProductAttributeValueId",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttributesValuesProductEntities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributesValuesProductEntities_ProductAttributeId",
                table: "AttributesValuesProductEntities",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributesValuesProductEntities_ProductAttributeValueId",
                table: "AttributesValuesProductEntities",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributesValuesProductEntities_ProductId",
                table: "AttributesValuesProductEntities",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributesValuesProductEntities");

            migrationBuilder.CreateTable(
                name: "ProductAttributeValuesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pieces = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeValueId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValuesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValuesProducts_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValuesProducts_ProductAttributeValues_ProductAttributeValueId",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValuesProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValuesProducts_ProductAttributeId",
                table: "ProductAttributeValuesProducts",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValuesProducts_ProductAttributeValueId",
                table: "ProductAttributeValuesProducts",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValuesProducts_ProductId",
                table: "ProductAttributeValuesProducts",
                column: "ProductId");
        }
    }
}
