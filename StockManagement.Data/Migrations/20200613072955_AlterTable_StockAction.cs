using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class AlterTable_StockAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockAction_CorrelationId",
                table: "StockAction");

            migrationBuilder.CreateIndex(
                name: "IX_StockAction_CorrelationId_StockActionType",
                table: "StockAction",
                columns: new[] { "CorrelationId", "StockActionType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockAction_CorrelationId_StockActionType",
                table: "StockAction");

            migrationBuilder.CreateIndex(
                name: "IX_StockAction_CorrelationId",
                table: "StockAction",
                column: "CorrelationId",
                unique: true);
        }
    }
}
