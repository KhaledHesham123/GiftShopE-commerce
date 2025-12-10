using ProductCatalogService.Data.EntitiesConfigurations;
using ProductCatalogService.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProductCatalogService.Data.DBContexts
{
    public class ProductCatalogDbContext : DbContext
    {
        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOccasion> ProductOccasions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CategoryConfiguration)));

        }
    }
}
