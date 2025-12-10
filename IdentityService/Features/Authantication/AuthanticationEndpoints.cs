using IdentityService.Features.Authantication.Commands.Forgetpassword;
using IdentityService.Features.Authantication.Commands.ResetPassword;

namespace IdentityService.Features.Authantication
{
    public static class AuthanticationEndpoints
    {

        public static IEndpointRouteBuilder MapAuthanticationEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapForgetpasswordEndpoint();
            app.MapResetPasswordEndpoint();

            return app;
        }
    }
}
