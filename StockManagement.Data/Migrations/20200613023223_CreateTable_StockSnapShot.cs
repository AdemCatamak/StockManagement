using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class CreateTable_StockSnapShot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         name: "StockSnapShot",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<long>(nullable: false)
                                                                         .Annotation("SqlServer:Identity", "1, 1"),
                                                               CreatedOn = table.Column<DateTime>(nullable: false),
                                                               UpdatedOn = table.Column<DateTime>(nullable: false),
                                                               ProductId = table.Column<long>(nullable: false),
                                                               AvailableStock = table.Column<int>(nullable: false),
                                                           },
                                         constraints: table => { table.PrimaryKey("PK_StockSnapShot", x => x.Id); });

            migrationBuilder.CreateIndex(
                                         name: "IX_StockSnapShot_ProductId",
                                         table: "StockSnapShot",
                                         column: "ProductId",
                                         unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "StockSnapShot");
        }
    }
}