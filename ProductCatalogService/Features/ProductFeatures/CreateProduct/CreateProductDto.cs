namespace ProductCatalogService.Features.ProductFeatures.CreateProduct
{
    public class CreateProductDto
    {
       public  string Name { get; set; }    
       public string Description { get; set; }
       public decimal Price { get; set; }
       public bool Status { get; set; }
       public Guid CategoryId { get; set; }
       public List<Guid> OccasionIds { get; set; } = new List<Guid>();
       public List<string> Tags { get; set; } = new List<string>();
       public List<IFormFile> Images { get; set; } = new List<IFormFile> ();
    }
}
