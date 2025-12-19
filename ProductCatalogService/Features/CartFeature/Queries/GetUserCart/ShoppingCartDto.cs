namespace ProductCatalogService.Features.CartFeature.Queries.GetUserCart
{
    public class ShoppingCartDto
    {
        public string Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<CartItemDto> Items { get; set; }

        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
    }
}
