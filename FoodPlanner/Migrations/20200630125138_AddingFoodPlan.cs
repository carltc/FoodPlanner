using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingFoodPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodPlanId",
                table: "Recipe",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FoodPlanId",
                table: "Ingredient",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FoodPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlanStart = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPlan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_FoodPlanId",
                table: "Recipe",
                column: "FoodPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_FoodPlanId",
                table: "Ingredient",
                column: "FoodPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_FoodPlan_FoodPlanId",
                table: "Ingredient",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_FoodPlan_FoodPlanId",
                table: "Recipe",
                column: "FoodPlanId",
                principalTable: "FoodPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_FoodPlan_FoodPlanId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_FoodPlan_FoodPlanId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "FoodPlan");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_FoodPlanId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_FoodPlanId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "FoodPlanId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "FoodPlanId",
                table: "Ingredient");
        }
    }
}
