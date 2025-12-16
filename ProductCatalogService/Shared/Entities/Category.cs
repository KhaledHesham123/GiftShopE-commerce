namespace ProductCatalogService.Shared.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; } // Active/NotActive
        public string? ImageUrl { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

}
