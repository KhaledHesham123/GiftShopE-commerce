using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogService.Shared.Entities;
using System.Reflection.Emit;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductTagConfigration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasOne(t => t.Product)
             .WithMany(p => p.Tags)
             .HasForeignKey(t => t.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
