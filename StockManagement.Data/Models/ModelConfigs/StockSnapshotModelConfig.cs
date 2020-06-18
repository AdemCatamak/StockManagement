using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockManagement.Data.Models.ModelConfigs
{
    public class StockSnapshotModelConfig : IEntityTypeConfiguration<StockSnapshotModel>
    {
        public void Configure(EntityTypeBuilder<StockSnapshotModel> builder)
        {
            builder.ToTable("StockSnapshot");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.ProductId)
                   .IsRequired();
            builder.Property(s => s.AvailableStock)
                   .IsRequired();
            builder.Property(s => s.CreatedOn)
                   .IsRequired();
            builder.Property(s => s.UpdatedOn)
                   .IsRequired();
            builder.Property(s => s.StockActionId)
                   .IsRequired();
            builder.Property(s => s.LastStockActionDate)
                   .IsRequired();

            builder.HasIndex(s => s.ProductId);
        }
    }
}