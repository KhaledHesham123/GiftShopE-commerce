using Microsoft.EntityFrameworkCore;
using OrderService.Shared.Entites;
using System.Reflection;

namespace OrderService.Data.DBContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options ):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }

        public DbSet<Order> Orders { get; set; }                                                            
        public DbSet<OrderItem> OrderItems { get; set; }                                                    
        public DbSet<OrderStatusLog> OrderStatusLogs { get; set; }


    }
}
