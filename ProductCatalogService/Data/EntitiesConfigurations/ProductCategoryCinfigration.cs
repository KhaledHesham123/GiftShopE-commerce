using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogService.Shared.Entities;
using System.Reflection.Emit;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class ProductCategoryCinfigration : IEntityTypeConfiguration<Shared.Entities.ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {

            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });


            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);


            builder.HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

        }
    }
}
