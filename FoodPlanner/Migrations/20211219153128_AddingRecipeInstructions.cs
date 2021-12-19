using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingRecipeInstructions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstructionText",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeSource",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructionText",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeSource",
                table: "Recipes");
        }
    }
}
