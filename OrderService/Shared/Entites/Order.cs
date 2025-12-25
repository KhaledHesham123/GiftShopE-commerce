using System.Runtime.Serialization;

namespace OrderService.Shared.Entites
{

    public enum OrderStatus
    {
        [EnumMember(Value = "Received")]
        Received = 1,
        [EnumMember(Value = "Preparing")]
        Preparing = 2,
        [EnumMember(Value = "OutForDelivery")]
        OutForDelivery = 3,
        [EnumMember(Value = "Delivered")]
        Delivered = 4,
        [EnumMember(Value = "Cancelled")]
        Cancelled = 5
    }

    public enum PaymentMethods
    {
        
        [EnumMember(Value = "CashonDelivery")]
        CashonDelivery = 1,
        [EnumMember(Value = "CreditCard")]
        CreditCard = 2,
        
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
        public int PointsRedeemed { get; set; }

        public PaymentMethods PaymentMethod { get; set; } = PaymentMethods.CreditCard;
        public OrderStatus Status { get; set; }

        public string? DeliveryHeroName { get; set; }
        public string? DeliveryHeroContact { get; set; }
        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }

        public DateTime? EstimatedArrivalTime { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }= new HashSet<OrderItem>();
        public ICollection<OrderStatusLog> StatusHistory { get; set; } = new HashSet<OrderStatusLog>();
    }
}
