using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Shared.Entities;

namespace UserProfileService.Data.EntitiesConfigurations
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(10,2)");

            builder.Property(x => x.DiscountPrice)
                   .HasColumnType("decimal(10,2)");

            builder.HasMany(p => p.Attributes)
              .WithOne(a => a.Product)
              .HasForeignKey(a => a.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Images)
                   .WithOne(i => i.Product)
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Tags)
                   .WithOne(t => t.Product)
                   .HasForeignKey(t => t.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
