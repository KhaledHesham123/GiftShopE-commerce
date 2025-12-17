using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserProfileService.Data.EntitiesConfigurations
{
    public class UserAddress : IEntityTypeConfiguration<UserProfileService.Shared.Entities.UserAddress>
    {
        public void Configure(EntityTypeBuilder<Shared.Entities.UserAddress> builder)
        {
            builder.Property(x => x.RecipientName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Governorate)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.BuildingNameOrNo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.FloorNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.ApartmentNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.Landmark)
                .HasMaxLength(200);
            builder.HasIndex(x => x.UserProfileId);
        }
    }
}