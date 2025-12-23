using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Shared.Entites;

namespace OrderService.Data.Configrations
{
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.OrderItems)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x=> x.StatusHistory)
                   .WithOne()
                   .HasForeignKey(sh => sh.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
