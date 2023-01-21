using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingRecipeStepsDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_RecipeStepAction_ActionId",
                table: "RecipeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_RecipeStepTarget_TargetId",
                table: "RecipeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStepTarget_Appliance_ApplianceId",
                table: "RecipeStepTarget");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStepTarget_ShopItems_IngredientId",
                table: "RecipeStepTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeStepTarget",
                table: "RecipeStepTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeStepAction",
                table: "RecipeStepAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appliance",
                table: "Appliance");

            migrationBuilder.RenameTable(
                name: "RecipeStepTarget",
                newName: "RecipeStepTargets");

            migrationBuilder.RenameTable(
                name: "RecipeStepAction",
                newName: "RecipeStepActions");

            migrationBuilder.RenameTable(
                name: "Appliance",
                newName: "Appliances");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeStepTarget_IngredientId",
                table: "RecipeStepTargets",
                newName: "IX_RecipeStepTargets_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeStepTarget_ApplianceId",
                table: "RecipeStepTargets",
                newName: "IX_RecipeStepTargets_ApplianceId");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "RecipeStep",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeStepTargets",
                table: "RecipeStepTargets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeStepActions",
                table: "RecipeStepActions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appliances",
                table: "Appliances",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStepTargets_Appliances_ApplianceId",
                table: "RecipeStepTargets",
                column: "ApplianceId",
                principalTable: "Appliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStepTargets_ShopItems_IngredientId",
                table: "RecipeStepTargets",
                column: "IngredientId",
                principalTable: "ShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_RecipeStepActions_ActionId",
                table: "RecipeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_RecipeStepTargets_TargetId",
                table: "RecipeStep");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStepTargets_Appliances_ApplianceId",
                table: "RecipeStepTargets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStepTargets_ShopItems_IngredientId",
                table: "RecipeStepTargets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeStepTargets",
                table: "RecipeStepTargets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeStepActions",
                table: "RecipeStepActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appliances",
                table: "Appliances");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "RecipeStep");

            migrationBuilder.RenameTable(
                name: "RecipeStepTargets",
                newName: "RecipeStepTarget");

            migrationBuilder.RenameTable(
                name: "RecipeStepActions",
                newName: "RecipeStepAction");

            migrationBuilder.RenameTable(
                name: "Appliances",
                newName: "Appliance");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeStepTargets_IngredientId",
                table: "RecipeStepTarget",
                newName: "IX_RecipeStepTarget_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeStepTargets_ApplianceId",
                table: "RecipeStepTarget",
                newName: "IX_RecipeStepTarget_ApplianceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeStepTarget",
                table: "RecipeStepTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeStepAction",
                table: "RecipeStepAction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appliance",
                table: "Appliance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_RecipeStepAction_ActionId",
                table: "RecipeStep",
                column: "ActionId",
                principalTable: "RecipeStepAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_RecipeStepTarget_TargetId",
                table: "RecipeStep",
                column: "TargetId",
                principalTable: "RecipeStepTarget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStepTarget_Appliance_ApplianceId",
                table: "RecipeStepTarget",
                column: "ApplianceId",
                principalTable: "Appliance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStepTarget_ShopItems_IngredientId",
                table: "RecipeStepTarget",
                column: "IngredientId",
                principalTable: "ShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
