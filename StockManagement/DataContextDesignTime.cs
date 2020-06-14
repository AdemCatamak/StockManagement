using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StockManagement.ConfigSection;
using StockManagement.ConfigSection.ConfigModels;
using StockManagement.Data;

namespace StockManagement
{
    public class DataContextDesignTime : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            DbOption selectedDbOption = AppConfigs.SelectedDbOption();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
            switch (selectedDbOption.DbType)
            {
                case DbTypes.SqlServer:
                    dbContextOptionsBuilder.UseSqlServer(selectedDbOption.ConnectionStr);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            DbContextOptions<DataContext> dbContextOptions = dbContextOptionsBuilder.Options;
            return new DataContext(dbContextOptions);
        }
    }
}