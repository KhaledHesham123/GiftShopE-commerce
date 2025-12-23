namespace OrderService.Shared.Entites
{

    public enum OrderStatus
    {
        Received = 1,
        Preparing = 2,
        OutForDelivery = 3,
        Delivered = 4,
        Cancelled = 5
    }
    public class Order:BaseEntity
    {
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
        public OrderStatus Status { get; set; }

        public string? DeliveryHeroName { get; set; }
        public string? DeliveryHeroContact { get; set; }
        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }= new HashSet<OrderItem>();
        public IEnumerable<OrderStatusLog> StatusHistory { get; set; } = new HashSet<OrderStatusLog>();
    }
}
