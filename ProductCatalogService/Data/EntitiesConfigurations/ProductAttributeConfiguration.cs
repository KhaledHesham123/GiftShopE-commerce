using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogService.Shared.Entities;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasOne(a => a.Product)
            .WithMany(p => p.Attributes)
             .HasForeignKey(a => a.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
