using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddingConsumable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsumableId",
                table: "Ingredient",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Consumable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_ConsumableId",
                table: "Ingredient",
                column: "ConsumableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Consumable_ConsumableId",
                table: "Ingredient",
                column: "ConsumableId",
                principalTable: "Consumable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Consumable_ConsumableId",
                table: "Ingredient");

            migrationBuilder.DropTable(
                name: "Consumable");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_ConsumableId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "ConsumableId",
                table: "Ingredient");
        }
    }
}
