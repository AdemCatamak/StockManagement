using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockManagement.Data.Models.ModelConfigs
{
    public class StockActionModelConfig : IEntityTypeConfiguration<StockActionModel>
    {
        public void Configure(EntityTypeBuilder<StockActionModel> builder)
        {
            builder.ToTable("StockAction");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.ProductId)
                   .IsRequired();
            builder.Property(s => s.StockActionType)
                   .IsRequired();
            builder.Property(s => s.Count)
                   .IsRequired();
            builder.Property(s => s.CorrelationId)
                   .IsRequired();
            builder.Property(s => s.CreatedOn)
                   .IsRequired();

            builder.HasIndex(s => s.ProductId);
            builder.HasIndex(s => new {s.CorrelationId, s.StockActionType})
                   .IsUnique();
        }
    }
}