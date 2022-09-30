using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace estoque_api.Migrations
{
    public partial class AddingSellingCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellingCount",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellingCount",
                table: "Products");
        }
    }
}
