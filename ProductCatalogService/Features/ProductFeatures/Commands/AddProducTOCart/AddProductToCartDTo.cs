namespace ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart
{
    public class AddProductToCartDTo
    {
        public Guid userid { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
