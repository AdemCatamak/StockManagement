using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class CreateTable_Stock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         name: "Stock",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<long>(nullable: false)
                                                                         .Annotation("SqlServer:Identity", "1, 1"),
                                                               CreatedOn = table.Column<DateTime>(nullable: false),
                                                               UpdatedOn = table.Column<DateTime>(nullable: false),
                                                               ProductId = table.Column<long>(nullable: false),
                                                               ProductCode = table.Column<string>(nullable: false),
                                                               AvailableStock = table.Column<int>(nullable: false),
                                                               StockActionId = table.Column<long>(nullable: false)
                                                           },
                                         constraints: table => { table.PrimaryKey("PK_Stock", x => x.Id); });


            migrationBuilder.CreateIndex(
                                         name: "IX_Stock_ProductId",
                                         table: "Stock",
                                         column: "ProductId",
                                         unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Stock");
        }
    }
}