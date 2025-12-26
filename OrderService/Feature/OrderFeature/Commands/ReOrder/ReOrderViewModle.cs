namespace OrderService.Feature.OrderFeature.Commands.ReOrder
{
    public class ReOrderViewModle
    {
       public Guid OrderId { get; set; }
        public string? NewShippingAddress { get; set; }
        public ICollection<ReOrderItemsOrderDto> Items { get; set; } = new HashSet<ReOrderItemsOrderDto>();
    }
}