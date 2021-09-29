using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddCuisines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cuisine_CuisineId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuisine",
                table: "Cuisine");

            migrationBuilder.RenameTable(
                name: "Cuisine",
                newName: "Cuisines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuisines",
                table: "Cuisines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cuisines_CuisineId",
                table: "Recipes",
                column: "CuisineId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cuisines_CuisineId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuisines",
                table: "Cuisines");

            migrationBuilder.RenameTable(
                name: "Cuisines",
                newName: "Cuisine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuisine",
                table: "Cuisine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cuisine_CuisineId",
                table: "Recipes",
                column: "CuisineId",
                principalTable: "Cuisine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
