using MediatR;

namespace IdentityService.Features.Authantication.Commands.VerifyEmailCode
{
    public static class VerifyEmailCodeEndpoint
    {
        public static IEndpointRouteBuilder MapVerifyEmailCodeEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/VerifyEmailCode", async (string Email,string token, IMediator mediator, HttpRequest request) =>
            {
               


                var response = await mediator.Send(new VerfyCodeOrchestrator(Email, token));

                if (!response.IsSuccess)
                {
                    return Results.Problem(
                        detail: string.Join("; ", response.Errors.Any() ? response.Errors : new[] { response.Message ?? "" }),
                        statusCode: response.StatusCode
                    );
                }

                return Results.Ok(response);
            });

            return app;
        }
    }
}
