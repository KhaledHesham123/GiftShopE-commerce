namespace ProductCatalogService.Features.ProductFeatures.UpdateProduct
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid> OccasionIds { get; set; } = new List<Guid>();
        public List<ProductAttributeDto> Attributes { get; set; } = new List<ProductAttributeDto>();
        public List<string> Tags { get; set; } = new List<string>();
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
