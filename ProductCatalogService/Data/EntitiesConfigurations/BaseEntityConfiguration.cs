using UserProfileService.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserProfileService.Data.EntitiesConfigurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnType("uniqueidentifier");

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(x => x.CreatedAt)
              .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}