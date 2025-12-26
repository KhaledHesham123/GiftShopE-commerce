using OrderService.Feature.OrderIemsFeature.Command.AddOrderItems;
using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public string ShippingAddress { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }

        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public int PointsRedeemed { get; set; }

        public string PaymentMethod { get; set; }
        public string Status { get; set; }

        public string? DeliveryHeroName { get; set; }
        public string? DeliveryHeroContact { get; set; }
        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
    }
}
