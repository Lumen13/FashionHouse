using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionHouse.Data.EF.Migrations
{
    public partial class deleteProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductAttributes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
