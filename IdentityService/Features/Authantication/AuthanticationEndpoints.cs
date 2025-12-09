using IdentityService.Features.Authantication.Commands.Forgetpassword;

namespace IdentityService.Features.Authantication
{
    public static class AuthanticationEndpoints
    {

        public static IEndpointRouteBuilder MapAuthanticationEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapForgetpasswordEndpoint();

            return app;
        }
    }
}
