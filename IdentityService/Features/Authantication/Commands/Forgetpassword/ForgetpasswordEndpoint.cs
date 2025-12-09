using IdentityService.Features.Authantication.Commands.Login;
using IdentityService.Shared.Services.EmailVerificationServices;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    public static class ForgetpasswordEndpoint
    {

        public static IEndpointRouteBuilder MapForgetpasswordEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/forgetpassword", async (string Email, IMediator mediator, HttpRequest request) =>
            {
                var scheme = request.Scheme;
                var host = request.Host.Value;
                var baseUrl = $"{scheme}://{host}";


                var response = await mediator.Send(new ForgetpasswordOrchestrator(Email, baseUrl));

                if (!response.IsSuccess)
                    return Results.NotFound(response.Message);

                return Results.Ok(response);
            });

            return app;
        }
    }
}
