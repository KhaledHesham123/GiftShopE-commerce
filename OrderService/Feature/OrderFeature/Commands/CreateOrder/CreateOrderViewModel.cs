using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    public class CreateOrderViewModel
    {
        public Guid UserId { get; set; }

        public string ShippingAddress { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhone { get; set; }

        public PaymentMethods PaymentMethod { get; set; }

        public int PointsRedeemed { get; set; }

        public double? CurrentLat { get; set; }

        public double? CurrentLng { get; set; }
    }
}