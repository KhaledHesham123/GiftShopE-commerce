namespace ProductCatalogService.Shared.Entities
{
    public class ProductOccasion : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid OccasionId { get; set; }
        public Occasion Occasion { get; set; }
    }

}
