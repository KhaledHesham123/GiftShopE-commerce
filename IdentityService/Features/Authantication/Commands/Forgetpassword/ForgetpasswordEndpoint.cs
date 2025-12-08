using IdentityService.Features.Authantication.Commands.Login;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    public static class ForgetpasswordEndpoint
    {
        public static IEndpointRouteBuilder MapForgetpasswordEndpoint(
        this IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/Forgetpassword",
                async (
                    LoginCommend command,
                    IMediator mediator,
                    CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                   

                    return Results.Ok(result);
                })
                .WithName("Forgetpassword")
                .WithTags("Authentication");

            return app;
        }
    }
}
