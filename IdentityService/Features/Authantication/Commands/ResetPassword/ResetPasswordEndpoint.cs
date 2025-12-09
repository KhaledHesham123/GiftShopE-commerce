using MediatR;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public static class ResetPasswordEndpoint
    {
        public static IEndpointRouteBuilder MapResetPasswordEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/ResetPassword", async (ResetPasswordViewModle resetPasswordViewModle ,IMediator mediator, HttpRequest request) =>
            {
                

                return Results.Ok();
            });

            return app;
        }
    }
}
