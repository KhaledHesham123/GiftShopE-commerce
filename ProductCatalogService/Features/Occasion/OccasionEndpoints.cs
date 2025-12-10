using ProductCatalogService.Features.Occasion.Commands.UpdateOccasion;

namespace ProductCatalogService.Features.Occasion
{
    public static class OccasionEndpoints
    {
        public static IEndpointRouteBuilder MapOccasionEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapUpdateOccasionEndpoint();

            return app;
        }
    }
}
