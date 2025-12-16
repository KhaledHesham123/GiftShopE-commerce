using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductImageConfigrations : IEntityTypeConfiguration<Shared.Entities.ProductImage>
    {
        public void Configure(EntityTypeBuilder<Shared.Entities.ProductImage> builder)
        {
            builder.HasOne(i => i.Product)
            .WithMany(p => p.Images)
             .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
