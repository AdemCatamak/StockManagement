using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class CreateTable_StockAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockAction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    StockActionType = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CorrelationId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockAction_CorrelationId",
                table: "StockAction",
                column: "CorrelationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockAction_ProductId",
                table: "StockAction",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockAction");
        }
    }
}
