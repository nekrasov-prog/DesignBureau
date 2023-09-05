using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignBureauWebApplication.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventory_EquipmentId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "EquipmentStatus",
                table: "Equipment");

            migrationBuilder.CreateTable(
                name: "EquipmentHistory",
                columns: table => new
                {
                    EquipmentHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    EquipmentStatus = table.Column<int>(type: "int", nullable: false),
                    StatusStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentHistory", x => x.EquipmentHistoryId);
                    table.ForeignKey(
                        name: "FK_EquipmentHistory_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_EquipmentId",
                table: "Inventory",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_EquipmentId",
                table: "EquipmentHistory",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentHistory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_EquipmentId",
                table: "Inventory");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentStatus",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_EquipmentId",
                table: "Inventory",
                column: "EquipmentId");
        }
    }
}
