namespace ProductCatalogService.Shared.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
