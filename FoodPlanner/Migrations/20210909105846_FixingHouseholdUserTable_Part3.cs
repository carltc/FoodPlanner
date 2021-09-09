using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Migrations
{
    public partial class FixingHouseholdUserTable_Part3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdUser_AspNetUsers_AppUserId",
                table: "HouseholdUser");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdUser_Households_HouseholdId",
                table: "HouseholdUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseholdUser",
                table: "HouseholdUser");

            migrationBuilder.RenameTable(
                name: "HouseholdUser",
                newName: "HouseholdUsers");

            migrationBuilder.RenameIndex(
                name: "IX_HouseholdUser_AppUserId",
                table: "HouseholdUsers",
                newName: "IX_HouseholdUsers_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseholdUsers",
                table: "HouseholdUsers",
                columns: new[] { "HouseholdId", "AppUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdUsers_AspNetUsers_AppUserId",
                table: "HouseholdUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdUsers_Households_HouseholdId",
                table: "HouseholdUsers",
                column: "HouseholdId",
                principalTable: "Households",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdUsers_AspNetUsers_AppUserId",
                table: "HouseholdUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdUsers_Households_HouseholdId",
                table: "HouseholdUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseholdUsers",
                table: "HouseholdUsers");

            migrationBuilder.RenameTable(
                name: "HouseholdUsers",
                newName: "HouseholdUser");

            migrationBuilder.RenameIndex(
                name: "IX_HouseholdUsers_AppUserId",
                table: "HouseholdUser",
                newName: "IX_HouseholdUser_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseholdUser",
                table: "HouseholdUser",
                columns: new[] { "HouseholdId", "AppUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdUser_AspNetUsers_AppUserId",
                table: "HouseholdUser",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdUser_Households_HouseholdId",
                table: "HouseholdUser",
                column: "HouseholdId",
                principalTable: "Households",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
