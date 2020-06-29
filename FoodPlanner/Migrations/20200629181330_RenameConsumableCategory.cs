using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class RenameConsumableCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.AddColumn<int>(
                name: "ConsumableId",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Consumable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
