using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockManagement.Data.Models.ModelConfigs
{
    public class ProductModelConfig : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProductCode)
                   .IsRequired();
            builder.Property(p => p.CreatedOn)
                   .IsRequired();

            builder.HasIndex(p => p.ProductCode);
        }
    }
}