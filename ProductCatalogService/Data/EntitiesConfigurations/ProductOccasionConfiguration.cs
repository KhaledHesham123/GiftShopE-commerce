using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogService.Shared.Entities;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductOccasionConfiguration : BaseEntityConfiguration<ProductOccasion>
    {
        public override void Configure(EntityTypeBuilder<ProductOccasion> builder)
        {
            base.Configure(builder);

            builder.HasOne(po => po.Product)
                   .WithMany(p => p.ProductOccasions)
                   .HasForeignKey(po => po.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(po => po.Occasion)
                   .WithMany(o => o.ProductOccasions)
                   .HasForeignKey(po => po.OccasionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
