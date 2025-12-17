using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Shared.Entities;

namespace UserProfileService.Data.EntitiesConfigurations
{
    public class OccasionConfiguration : BaseEntityConfiguration<Occasion>
    {
        public override void Configure(EntityTypeBuilder<Occasion> builder)
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
