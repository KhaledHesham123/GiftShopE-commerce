namespace ProductCatalogService.Shared.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}