namespace OrderService.Feature.OrderIemsFeature.Command.AddOrderItems
{
    public class OrderItemDto
    {
        public Guid id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
