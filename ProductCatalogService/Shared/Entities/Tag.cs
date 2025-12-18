namespace ProductCatalogService.Shared.Entities
{
    public class Tag : BaseEntity
    {
         public string Name { get; set; }= string.Empty;
        public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
    }
}