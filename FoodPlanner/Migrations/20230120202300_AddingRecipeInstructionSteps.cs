using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingRecipeInstructionSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructionsId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appliance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appliance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInstructions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeStepAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeStepAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeStepTarget",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplianceId = table.Column<int>(nullable: true),
                    IngredientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeStepTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeStepTarget_Appliance_ApplianceId",
                        column: x => x.ApplianceId,
                        principalTable: "Appliance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeStepTarget_ShopItems_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "ShopItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeStep",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreText = table.Column<string>(nullable: true),
                    ActionId = table.Column<int>(nullable: true),
                    MidText = table.Column<string>(nullable: true),
                    TargetId = table.Column<int>(nullable: true),
                    PostText = table.Column<string>(nullable: true),
                    RecipeInstructionsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeStep_RecipeStepAction_ActionId",
                        column: x => x.ActionId,
                        principalTable: "RecipeStepAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeStep_RecipeInstructions_RecipeInstructionsId",
                        column: x => x.RecipeInstructionsId,
                        principalTable: "RecipeInstructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeStep_RecipeStepTarget_TargetId",
                        column: x => x.TargetId,
                        principalTable: "RecipeStepTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_InstructionsId",
                table: "Recipes",
                column: "InstructionsId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_ActionId",
                table: "RecipeStep",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_RecipeInstructionsId",
                table: "RecipeStep",
                column: "RecipeInstructionsId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_TargetId",
                table: "RecipeStep",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStepTarget_ApplianceId",
                table: "RecipeStepTarget",
                column: "ApplianceId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStepTarget_IngredientId",
                table: "RecipeStepTarget",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeInstructions_InstructionsId",
                table: "Recipes",
                column: "InstructionsId",
                principalTable: "RecipeInstructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeInstructions_InstructionsId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeStep");

            migrationBuilder.DropTable(
                name: "RecipeStepAction");

            migrationBuilder.DropTable(
                name: "RecipeInstructions");

            migrationBuilder.DropTable(
                name: "RecipeStepTarget");

            migrationBuilder.DropTable(
                name: "Appliance");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_InstructionsId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "InstructionsId",
                table: "Recipes");
        }
    }
}
