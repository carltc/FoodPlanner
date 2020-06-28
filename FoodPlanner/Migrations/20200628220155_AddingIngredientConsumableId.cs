using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingIngredientConsumableId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Consumable_ConsumableId",
                table: "Ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "ConsumableId",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Consumable_ConsumableId",
                table: "Ingredient",
                column: "ConsumableId",
                principalTable: "Consumable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Consumable_ConsumableId",
                table: "Ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "ConsumableId",
                table: "Ingredient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Consumable_ConsumableId",
                table: "Ingredient",
                column: "ConsumableId",
                principalTable: "Consumable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
