using UserProfileService.Features.UserProfileFeature.Quries.GetUserByid;

namespace UserProfileService.Features.UserProfileFeature
{
    public static class UserProfileEndpoints
    {
        public static IEndpointRouteBuilder MapAuthanticationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetUserByidEndpoint();

            return app;
        }
    }
}
