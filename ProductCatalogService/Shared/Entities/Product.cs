namespace ProductCatalogService.Shared.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public bool IsBestSeller { get; set; }
        public string ?ImageUrl { get; set; }
        public bool Status { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductOccasion> ProductOccasions { get; set; }
    }

}
