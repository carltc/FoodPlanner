using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddIngredientUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Ingredient");
        }
    }
}
