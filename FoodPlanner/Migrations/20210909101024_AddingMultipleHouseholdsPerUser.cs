using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingMultipleHouseholdsPerUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Household_HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlanRecipe_FoodPlan_FoodPlanId",
                table: "FoodPlanRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlanRecipe_Recipe_RecipeId",
                table: "FoodPlanRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Recipe_RecipeId",
                table: "ShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Product_ProductId",
                table: "ShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopTrip_FoodPlan_FoodPlanId",
                table: "ShopTrip");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopTrip",
                table: "ShopTrip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductType",
                table: "ProductType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Household",
                table: "Household");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPlanRecipe",
                table: "FoodPlanRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPlan",
                table: "FoodPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ShopTrip",
                newName: "ShopTrips");

            migrationBuilder.RenameTable(
                name: "ShopItem",
                newName: "ShopItems");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameTable(
                name: "ProductType",
                newName: "ProductTypes");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Household",
                newName: "Households");

            migrationBuilder.RenameTable(
                name: "FoodPlanRecipe",
                newName: "FoodPlanRecipes");

            migrationBuilder.RenameTable(
                name: "FoodPlan",
                newName: "FoodPlans");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categorys");

            migrationBuilder.RenameIndex(
                name: "IX_ShopTrip_FoodPlanId",
                table: "ShopTrips",
                newName: "IX_ShopTrips_FoodPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_ProductId",
                table: "ShopItems",
                newName: "IX_ShopItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_RecipeId",
                table: "ShopItems",
                newName: "IX_ShopItems_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_FoodPlanId",
                table: "ShopItems",
                newName: "IX_ShopItems_FoodPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductTypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlanRecipe_RecipeId",
                table: "FoodPlanRecipes",
                newName: "IX_FoodPlanRecipes_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlanRecipe_FoodPlanId",
                table: "FoodPlanRecipes",
                newName: "IX_FoodPlanRecipes_FoodPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopTrips",
                table: "ShopTrips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItems",
                table: "ShopItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Households",
                table: "Households",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPlanRecipes",
                table: "FoodPlanRecipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPlans",
                table: "FoodPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HouseholdUsers",
                columns: table => new
                {
                    HouseholdId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdUsers", x => new { x.HouseholdId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_HouseholdUsers_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HouseholdUsers_Households_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdUsers_AppUserId1",
                table: "HouseholdUsers",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlanRecipes_FoodPlans_FoodPlanId",
                table: "FoodPlanRecipes",
                column: "FoodPlanId",
                principalTable: "FoodPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlanRecipes_Recipes_RecipeId",
                table: "FoodPlanRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categorys_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_FoodPlans_FoodPlanId",
                table: "ShopItems",
                column: "FoodPlanId",
                principalTable: "FoodPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Recipes_RecipeId",
                table: "ShopItems",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Products_ProductId",
                table: "ShopItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopTrips_FoodPlans_FoodPlanId",
                table: "ShopTrips",
                column: "FoodPlanId",
                principalTable: "FoodPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlanRecipes_FoodPlans_FoodPlanId",
                table: "FoodPlanRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlanRecipes_Recipes_RecipeId",
                table: "FoodPlanRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categorys_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_FoodPlans_FoodPlanId",
                table: "ShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Recipes_RecipeId",
                table: "ShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Products_ProductId",
                table: "ShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopTrips_FoodPlans_FoodPlanId",
                table: "ShopTrips");

            migrationBuilder.DropTable(
                name: "HouseholdUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopTrips",
                table: "ShopTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItems",
                table: "ShopItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Households",
                table: "Households");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPlans",
                table: "FoodPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPlanRecipes",
                table: "FoodPlanRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys");

            migrationBuilder.RenameTable(
                name: "ShopTrips",
                newName: "ShopTrip");

            migrationBuilder.RenameTable(
                name: "ShopItems",
                newName: "ShopItem");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameTable(
                name: "ProductTypes",
                newName: "ProductType");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Households",
                newName: "Household");

            migrationBuilder.RenameTable(
                name: "FoodPlans",
                newName: "FoodPlan");

            migrationBuilder.RenameTable(
                name: "FoodPlanRecipes",
                newName: "FoodPlanRecipe");

            migrationBuilder.RenameTable(
                name: "Categorys",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_ShopTrips_FoodPlanId",
                table: "ShopTrip",
                newName: "IX_ShopTrip_FoodPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItems_ProductId",
                table: "ShopItem",
                newName: "IX_ShopItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItems_RecipeId",
                table: "ShopItem",
                newName: "IX_ShopItem_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItems_FoodPlanId",
                table: "ShopItem",
                newName: "IX_ShopItem_FoodPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Product",
                newName: "IX_Product_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlanRecipes_RecipeId",
                table: "FoodPlanRecipe",
                newName: "IX_FoodPlanRecipe_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlanRecipes_FoodPlanId",
                table: "FoodPlanRecipe",
                newName: "IX_FoodPlanRecipe_FoodPlanId");

            migrationBuilder.AddColumn<int>(
                name: "HouseholdId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopTrip",
                table: "ShopTrip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductType",
                table: "ProductType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Household",
                table: "Household",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPlan",
                table: "FoodPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPlanRecipe",
                table: "FoodPlanRecipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HouseholdId",
                table: "AspNetUsers",
                column: "HouseholdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Household_HouseholdId",
                table: "AspNetUsers",
                column: "HouseholdId",
                principalTable: "Household",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlanRecipe_FoodPlan_FoodPlanId",
                table: "FoodPlanRecipe",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlanRecipe_Recipe_RecipeId",
                table: "FoodPlanRecipe",
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Recipe_RecipeId",
                table: "ShopItem",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Product_ProductId",
                table: "ShopItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopTrip_FoodPlan_FoodPlanId",
                table: "ShopTrip",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
