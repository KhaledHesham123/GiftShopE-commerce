namespace ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto
{
    public class OccasionDro
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string? ImageUrl { get; set; }
    }
}
