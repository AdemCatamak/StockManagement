using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Data.Migrations
{
    public partial class InsertInto_StockActionTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("StockActionTypes",
                                        new string[] {"Id", "Name"},
                                        new object[] {1, "InitializeStock"});

            migrationBuilder.InsertData("StockActionTypes",
                                        new string[] {"Id", "Name"},
                                        new object[] {2, "AddToStock"});

            migrationBuilder.InsertData("StockActionTypes",
                                        new string[] {"Id", "Name"},
                                        new object[] {3, "RemoveFromStock"});

            migrationBuilder.InsertData("StockActionTypes",
                                        new string[] {"Id", "Name"},
                                        new object[] {4, "ResetStock"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("StockActionTypes", new string[] {"Id"}, new object[] {1});
            migrationBuilder.DeleteData("StockActionTypes", new string[] {"Id"}, new object[] {2});
            migrationBuilder.DeleteData("StockActionTypes", new string[] {"Id"}, new object[] {3});
            migrationBuilder.DeleteData("StockActionTypes", new string[] {"Id"}, new object[] {4});
        }
    }
}