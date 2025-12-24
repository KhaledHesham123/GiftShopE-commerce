namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
