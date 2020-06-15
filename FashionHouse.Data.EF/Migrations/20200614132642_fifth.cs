using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionHouse.Data.EF.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Sellers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductAttributeId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductAttributeId",
                table: "Products",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductAttributes_ProductAttributeId",
                table: "Products",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductAttributes_ProductAttributeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductAttributeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
