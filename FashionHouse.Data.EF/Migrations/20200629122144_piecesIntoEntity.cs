using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionHouse.Data.EF.Migrations
{
    public partial class piecesIntoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pieces",
                table: "ProductAttributeValuesProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pieces",
                table: "ProductAttributeValuesProducts");
        }
    }
}
