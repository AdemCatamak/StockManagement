using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class CreateTable_StockActionTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         name: "StockActionTypes",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<long>(nullable: false),
                                                               Name = table.Column<string>(nullable: false)
                                                           }
                                        );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("StockActionTypes");
        }
    }
}