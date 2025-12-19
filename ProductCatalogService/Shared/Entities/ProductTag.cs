namespace ProductCatalogService.Shared.Entities
{
    public class ProductTag:BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}