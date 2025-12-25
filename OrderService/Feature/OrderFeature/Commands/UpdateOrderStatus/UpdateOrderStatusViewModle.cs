using OrderService.Shared.Entites;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusViewModle
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public string? DeliveryHeroName { get; set; }
        public string? DeliveryHeroContact { get; set; }
        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }
        public DateTime? EstimatedArrivalTime { get; set; }
    }
}