using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class AlterTables_Stock_StockSnapshot__LastStockOperationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastStockActionDate",
                table: "StockSnapshot",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Stock",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStockOperationDate",
                table: "Stock",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Stock_LastStockOperationDate",
                table: "Stock",
                column: "LastStockOperationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductCode",
                table: "Stock",
                column: "ProductCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_LastStockOperationDate",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_ProductCode",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "LastStockActionDate",
                table: "StockSnapshot");

            migrationBuilder.DropColumn(
                name: "LastStockOperationDate",
                table: "Stock");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
