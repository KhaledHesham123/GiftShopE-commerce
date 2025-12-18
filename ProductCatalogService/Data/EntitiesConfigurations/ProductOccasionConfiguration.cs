using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Shared.Entities;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductOccasionConfiguration : IEntityTypeConfiguration<ProductOccasion>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductOccasion> builder)
        {
            builder.HasKey(po => new { po.ProductId, po.OccasionId });

            builder.HasOne(po => po.Product)
                .WithMany(p => p.ProductOccasions)
                .HasForeignKey(po => po.ProductId);

            builder.HasOne(po => po.Occasion)
                .WithMany(o => o.ProductOccasions)
                .HasForeignKey(po => po.OccasionId);

        }
    }
}
