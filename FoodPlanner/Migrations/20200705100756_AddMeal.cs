using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanStart",
                table: "FoodPlan");

            migrationBuilder.AddColumn<int>(
                name: "Meal",
                table: "ShopItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Meal",
                table: "FoodPlanRecipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FoodPlan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meal",
                table: "ShopItem");

            migrationBuilder.DropColumn(
                name: "Meal",
                table: "FoodPlanRecipe");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FoodPlan");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanStart",
                table: "FoodPlan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
