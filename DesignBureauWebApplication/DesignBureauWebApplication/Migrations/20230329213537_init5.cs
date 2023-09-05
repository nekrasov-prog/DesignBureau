using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignBureauWebApplication.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventory_MaterialId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Assignment");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MaterialId",
                table: "Inventory",
                column: "MaterialId",
                unique: true,
                filter: "[MaterialId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventory_MaterialId",
                table: "Inventory");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Assignment",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MaterialId",
                table: "Inventory",
                column: "MaterialId");
        }
    }
}
