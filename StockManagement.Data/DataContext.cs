using Microsoft.EntityFrameworkCore;
using StockManagement.Data.Models;

namespace StockManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ProductModel> ProductModels { get; set; }
        public virtual DbSet<StockActionModel> StockActionModels { get; set; }
        public virtual DbSet<StockSnapShotModel> StockSnapShotModels { get; set; }
        public virtual DbSet<StockModel> StockModels { get; set; }
    }
}