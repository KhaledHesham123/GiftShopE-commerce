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

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                   .IsRequired();



            builder.Property(u => u.EmailConfirmed)
                   .HasDefaultValue(false);




            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.HasIndex(u => u.Username)
                   .IsUnique();



            builder.HasMany(x => x.UserRoles).WithOne(x => x.user).HasForeignKey(x => x.Userid).OnDelete(DeleteBehavior.Cascade);



            builder.HasMany(x => x.refreshTokens).WithOne(x => x.User).HasForeignKey(x => x.userid).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
