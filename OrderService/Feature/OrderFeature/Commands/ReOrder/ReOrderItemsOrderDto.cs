namespace OrderService.Feature.OrderFeature.Commands.ReOrder
{
    public class ReOrderItemsOrderDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
