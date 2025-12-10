using IdentityService.Shared.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.Configrations
{
    public class RefreshTokensConfigrations: IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            //builder.Property(rt => rt.userId)
            //      .IsRequired();


            builder.Property(rt => rt.Token)
                   .IsRequired()
                   .HasMaxLength(500);


            //builder.Property(rt => rt.IsUsed)
            //       .HasDefaultValue(false);

            builder.Property(rt => rt.ExpiresOn)
                   .IsRequired();

            builder.Property(rt => rt.CreatedAt)
                   .IsRequired();

            builder.Property(rt => rt.RevokedOn)
                   .IsRequired(false);

            builder.HasIndex(rt => rt.Token)
                   .IsUnique();
        }
    }
}
