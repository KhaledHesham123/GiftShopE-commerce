using OrderService.Feature.OrderStatusLogFeature.Commands.UpdateOrderStatusLog;
using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus.DTOs
{
    public class OrderTrackingToReturnDto
    {
        public string OrderNumber { get; set; }
        public OrderStatus CurrentStatus { get; set; }

        public DeliveryLocationDto? DeliveryLocation { get; set; }

        public ICollection<OrderStatusLogDto> StatusHistory { get; set; } = new HashSet<OrderStatusLogDto>();
    }
}
