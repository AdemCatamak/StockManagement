using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class AlterTable_StockSnapShot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                                             name: "StockActionId",
                                             table: "StockSnapShot",
                                             nullable: false,
                                             defaultValue: 0L);

            migrationBuilder.CreateIndex(
                                         name: "IX_StockSnapShot_StockActionId",
                                         table: "StockSnapShot",
                                         column: "StockActionId",
                                         unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                                       name: "IX_StockSnapShot_StockActionId",
                                       table: "StockSnapShot");

            migrationBuilder.DropColumn(
                                        name: "StockActionId",
                                        table: "StockSnapShot");
        }
    }
}