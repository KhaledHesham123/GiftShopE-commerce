namespace OrderService.Shared.Entites
{
    public class OrderStatusLog : BaseEntity
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}