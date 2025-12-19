namespace ProductCatalogService.Features.CartFeature.Queries.GetUserCart
{
    public class ShoppingCartDto
    {
        public string Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<CartItemDto> Items { get; set; }
    }
}
