namespace ProductCatalogService.Features.Occasion.Commands.UpdateOccasion
{
    public class UpdateOccasionModle
    {
        public Guid id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool Status { get; set; }
    }
}
