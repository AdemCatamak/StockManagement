using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class AlterTable_StockSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                                             name: "StockActionId",
                                             table: "StockSnapshot",
                                             nullable: false,
                                             defaultValue: 0L);

            migrationBuilder.CreateIndex(
                                         name: "IX_StockSnapshot_StockActionId",
                                         table: "StockSnapshot",
                                         column: "StockActionId",
                                         unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                                       name: "IX_StockSnapshot_StockActionId",
                                       table: "StockSnapshot");

            migrationBuilder.DropColumn(
                                        name: "StockActionId",
                                        table: "StockSnapshot");
        }
    }
}