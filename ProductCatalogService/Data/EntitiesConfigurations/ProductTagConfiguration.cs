using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogService.Shared.Entities;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasOne(pt => pt.Product)
                   .WithMany(p => p.ProductTags)
                   .HasForeignKey(pt => pt.ProductId);

            builder.HasOne(pt => pt.Tag)
                   .WithMany(t => t.ProductTags)
                   .HasForeignKey(pt => pt.TagId);
        }
    }
}
