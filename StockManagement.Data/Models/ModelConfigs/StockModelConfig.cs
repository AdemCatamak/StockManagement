using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockManagement.Data.Models.ModelConfigs
{
    public class StockModelConfig : IEntityTypeConfiguration<StockModel>
    {
        public void Configure(EntityTypeBuilder<StockModel> builder)
        {
            builder.ToTable("Stock");

            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.CreatedOn)
                   .IsRequired();
            builder.Property(s => s.UpdatedOn)
                   .IsRequired();
            builder.Property(s => s.ProductId)
                   .IsRequired();
            builder.Property(s => s.ProductCode)
                   .IsRequired();
            builder.Property(s => s.StockActionId)
                   .IsRequired();
            builder.Property(s => s.AvailableStock)
                   .IsRequired();
            builder.Property(s => s.LastStockOperationDate)
                   .IsRequired();

            builder.HasIndex(s => s.ProductId)
                   .IsUnique();
            builder.HasIndex(s => s.ProductCode)
                   .IsUnique();
            builder.HasIndex(s => s.LastStockOperationDate);
        }
    }
}