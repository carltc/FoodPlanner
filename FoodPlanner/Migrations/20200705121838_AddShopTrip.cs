using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class AddShopTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopTrip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopTripCompleted = table.Column<DateTime>(nullable: false),
                    FoodPlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTrip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopTrip_FoodPlan_FoodPlanId",
                        column: x => x.FoodPlanId,
                        principalTable: "FoodPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopTrip_FoodPlanId",
                table: "ShopTrip",
                column: "FoodPlanId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopTrip");
        }
    }
}
