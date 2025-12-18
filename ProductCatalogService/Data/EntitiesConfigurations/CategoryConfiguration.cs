using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogService.Shared.Entities;

namespace ProductCatalogService.Data.EntitiesConfigurations
{
    public class CategoryConfiguration  : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                          .IsRequired()
                          .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.Property(x => x.Status)
                .HasDefaultValue(true);
        }
    }
}
