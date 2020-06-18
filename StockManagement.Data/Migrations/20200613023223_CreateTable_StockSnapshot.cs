using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class CreateTable_StockSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         name: "StockSnapshot",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<long>(nullable: false)
                                                                         .Annotation("SqlServer:Identity", "1, 1"),
                                                               CreatedOn = table.Column<DateTime>(nullable: false),
                                                               UpdatedOn = table.Column<DateTime>(nullable: false),
                                                               ProductId = table.Column<long>(nullable: false),
                                                               AvailableStock = table.Column<int>(nullable: false),
                                                           },
                                         constraints: table => { table.PrimaryKey("PK_StockSnapshot", x => x.Id); });

            migrationBuilder.CreateIndex(
                                         name: "IX_StockSnapshot_ProductId",
                                         table: "StockSnapshot",
                                         column: "ProductId",
                                         unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "StockSnapshot");
        }
    }
}