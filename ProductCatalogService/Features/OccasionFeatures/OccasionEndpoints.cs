using UserProfileService.Features.OccasionFeatures.Commands.UpdateOccasion;

namespace UserProfileService.Features.OccasionFeatures
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
