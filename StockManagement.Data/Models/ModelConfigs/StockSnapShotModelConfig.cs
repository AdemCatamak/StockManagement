using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockManagement.Data.Models.ModelConfigs
{
    public class StockSnapShotModelConfig : IEntityTypeConfiguration<StockSnapShotModel>
    {
        public void Configure(EntityTypeBuilder<StockSnapShotModel> builder)
        {
            builder.ToTable("StockSnapShot");

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

            builder.HasIndex(s => s.ProductId);
        }
    }
}