using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionHouse.Data.EF.Migrations
{
    public partial class attributeOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relevance",
                table: "ProductAttributeValues");

            migrationBuilder.AddColumn<string>(
                name: "AttributeValue",
                table: "ProductAttributeValues",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "ProductAttributeValues");

            migrationBuilder.AddColumn<string>(
                name: "Relevance",
                table: "ProductAttributeValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
