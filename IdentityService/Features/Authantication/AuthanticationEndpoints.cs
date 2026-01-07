using IdentityService.Features.Authantication.Commands.Forgetpassword;
using IdentityService.Features.Authantication.Commands.ResetPassword;
using IdentityService.Features.Authantication.Commands.VerifyEmailCode;

namespace IdentityService.Features.Authantication
{
    public static class AuthanticationEndpoints
    {

        public static IEndpointRouteBuilder MapAuthanticationEndpoints(this IEndpointRouteBuilder app) 
        {
           
            return app;
        }
    }
}
