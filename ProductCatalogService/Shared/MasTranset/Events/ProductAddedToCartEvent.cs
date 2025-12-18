namespace ProductCatalogService.Shared.MasTranset.Events
{
    public class ProductAddedToCartEvent
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string ProductImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
