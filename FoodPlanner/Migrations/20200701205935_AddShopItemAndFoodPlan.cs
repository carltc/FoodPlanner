using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddShopItemAndFoodPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_FoodPlan_FoodPlanId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Product_ProductId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_FoodPlan_FoodPlanId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_FoodPlanId",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "FoodPlanId",
                table: "Recipe");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "ShopItem");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_RecipeId",
                table: "ShopItem",
                newName: "IX_ShopItem_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_ProductId",
                table: "ShopItem",
                newName: "IX_ShopItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_FoodPlanId",
                table: "ShopItem",
                newName: "IX_ShopItem_FoodPlanId");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "ShopItem",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ShopItem",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FoodPlanRecipe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodPlanId = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPlanRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodPlanRecipe_FoodPlan_FoodPlanId",
                        column: x => x.FoodPlanId,
                        principalTable: "FoodPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodPlanRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlanRecipe_FoodPlanId",
                table: "FoodPlanRecipe",
                column: "FoodPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlanRecipe_RecipeId",
                table: "FoodPlanRecipe",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Recipe_RecipeId",
                table: "ShopItem",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Product_ProductId",
                table: "ShopItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Recipe_RecipeId",
                table: "ShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Product_ProductId",
                table: "ShopItem");

            migrationBuilder.DropTable(
                name: "FoodPlanRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ShopItem");

            migrationBuilder.RenameTable(
                name: "ShopItem",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_ProductId",
                table: "Ingredient",
                newName: "IX_Ingredient_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_FoodPlanId",
                table: "Ingredient",
                newName: "IX_Ingredient_FoodPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_RecipeId",
                table: "Ingredient",
                newName: "IX_Ingredient_RecipeId");

            migrationBuilder.AddColumn<int>(
                name: "FoodPlanId",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Ingredient",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_FoodPlanId",
                table: "Recipe",
                column: "FoodPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_FoodPlan_FoodPlanId",
                table: "Ingredient",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Recipe_FoodPlan_FoodPlanId",
                table: "Recipe",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
