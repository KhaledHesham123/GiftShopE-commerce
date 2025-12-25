using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderStatusLogFeature.Commands.UpdateOrderStatusLog
{
    public class OrderStatusLogDto
    {
        public OrderStatus Status { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}