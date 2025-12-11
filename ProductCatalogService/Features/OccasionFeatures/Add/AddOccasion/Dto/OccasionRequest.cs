namespace ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto
{
    public class OccasionRequest
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
