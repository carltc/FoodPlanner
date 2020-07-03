using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class ChangeFoodPlanShopItesmToFoodPlanProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_FoodPlan_FoodPlanId",
                table: "ShopItem",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
