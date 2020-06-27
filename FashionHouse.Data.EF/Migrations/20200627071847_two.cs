using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionHouse.Data.EF.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductAttributeId",
                table: "ProductAttributeValuesProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValuesProducts_ProductAttributeId",
                table: "ProductAttributeValuesProducts",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValuesProducts_ProductAttributes_ProductAttributeId",
                table: "ProductAttributeValuesProducts",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValuesProducts_ProductAttributes_ProductAttributeId",
                table: "ProductAttributeValuesProducts");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValuesProducts_ProductAttributeId",
                table: "ProductAttributeValuesProducts");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "ProductAttributeValuesProducts");
        }
    }
}
