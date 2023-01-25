using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class SimplifyingRecipeSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_RecipeStepActions_ActionId",
                table: "RecipeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_RecipeStepTargets_TargetId",
                table: "RecipeStep");

            migrationBuilder.DropIndex(
                name: "IX_RecipeStep_ActionId",
                table: "RecipeStep");

            migrationBuilder.DropIndex(
                name: "IX_RecipeStep_TargetId",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "MidText",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "PostText",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "PreText",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "RecipeStep");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "RecipeStep",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "RecipeStep");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShopItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "RecipeStep",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MidText",
                table: "RecipeStep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostText",
                table: "RecipeStep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreText",
                table: "RecipeStep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "RecipeStep",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_ActionId",
                table: "RecipeStep",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_TargetId",
                table: "RecipeStep",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_RecipeStepActions_ActionId",
                table: "RecipeStep",
                column: "ActionId",
                principalTable: "RecipeStepActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_RecipeStepTargets_TargetId",
                table: "RecipeStep",
                column: "TargetId",
                principalTable: "RecipeStepTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
