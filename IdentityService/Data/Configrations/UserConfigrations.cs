using IdentityService.Shared.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.Configrations
{
    public class UserConfigrations : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            //builder.Property(u => u.IsEmailConfirmed)
            //       .HasDefaultValue(false);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            //builder.HasIndex(u => u.Username)
            //       .IsUnique();

            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(x => x.RefreshTokens).WithOne(x => x.User).HasForeignKey(x => x.userid).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
