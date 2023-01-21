using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingRecipeStepFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStepTargets_Appliances_ApplianceId",
                table: "RecipeStepTargets");

            migrationBuilder.DropTable(
                name: "Appliances");

            migrationBuilder.DropIndex(
                name: "IX_RecipeStepTargets_ApplianceId",
                table: "RecipeStepTargets");

            migrationBuilder.DropColumn(
                name: "ApplianceId",
                table: "RecipeStepTargets");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "RecipeStepTargets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeStepTargetItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeStepTargetItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStepTargets_ItemId",
                table: "RecipeStepTargets",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStepTargets_RecipeStepTargetItems_ItemId",
                table: "RecipeStepTargets",
                column: "ItemId",
                principalTable: "RecipeStepTargetItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStepTargets_RecipeStepTargetItems_ItemId",
                table: "RecipeStepTargets");

            migrationBuilder.DropTable(
                name: "RecipeStepTargetItems");

            migrationBuilder.DropIndex(
                name: "IX_RecipeStepTargets_ItemId",
                table: "RecipeStepTargets");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "RecipeStepTargets");

            migrationBuilder.AddColumn<int>(
                name: "ApplianceId",
                table: "RecipeStepTargets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appliances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStepTargets_ApplianceId",
                table: "RecipeStepTargets",
                column: "ApplianceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStepTargets_Appliances_ApplianceId",
                table: "RecipeStepTargets",
                column: "ApplianceId",
                principalTable: "Appliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
