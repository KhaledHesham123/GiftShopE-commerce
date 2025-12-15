namespace CartService.Shared.MasTranset
{
    public class ProductAddedToCartEvent
    {
        public string ShoppingCartId { get; init; }
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string ProductImageUrl { get; set; }
        public int Quantity { get; set; }

    }
}
