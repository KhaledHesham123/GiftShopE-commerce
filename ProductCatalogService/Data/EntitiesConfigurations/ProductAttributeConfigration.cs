using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductAttributeConfigration : IEntityTypeConfiguration<Shared.Entities.ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<Shared.Entities.ProductAttribute> builder)
        {
            builder.HasOne(a => a.Product)
            .WithMany(p => p.Attributes)
             .HasForeignKey(a => a.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
