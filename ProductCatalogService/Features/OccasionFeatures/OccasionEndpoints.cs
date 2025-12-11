using ProductCatalogService.Features.OccasionFeatures.Commands.UpdateOccasion;

namespace ProductCatalogService.Features.OccasionFeatures
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
