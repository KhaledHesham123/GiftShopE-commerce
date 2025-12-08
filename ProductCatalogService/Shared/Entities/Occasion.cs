namespace ProductCatalogService.Shared.Entities
{
    public class Occasion : BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public string ?ImageUrl { get; set; }

        public ICollection<ProductOccasion> ProductOccasions { get; set; }
    }

}
