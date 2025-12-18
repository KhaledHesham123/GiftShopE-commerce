using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Shared.Entities;

namespace ProductCatalogService.Data.DBContexts
{
    public class UserProfileDbContext : DbContext
    {
        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options)
        {
        }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserAddress)));
        }
    }
}
