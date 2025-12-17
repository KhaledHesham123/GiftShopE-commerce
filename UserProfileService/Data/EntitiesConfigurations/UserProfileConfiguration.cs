using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Shared.Entities;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.Property(x => x.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);
        builder.Property(x => x.LastName)
                        .IsRequired()
                        .HasMaxLength(50);
            builder.Property(x => x.Gender)
                    .HasConversion<string>();
    }
}