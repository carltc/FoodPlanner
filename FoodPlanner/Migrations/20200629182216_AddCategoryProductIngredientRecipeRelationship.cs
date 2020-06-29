using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddCategoryProductIngredientRecipeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_ProductId",
                table: "Ingredient",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Product_ProductId",
                table: "Ingredient",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Product_ProductId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_ProductId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Ingredient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
