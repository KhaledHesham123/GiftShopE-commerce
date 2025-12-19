namespace ProductCatalogService.Features.CartFeature.Queries.GetUserCart
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }

        public string ShoppingCartId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}