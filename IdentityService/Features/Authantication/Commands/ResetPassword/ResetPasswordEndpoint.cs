using Domain_Layer.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public static class ResetPasswordEndpoint
    {
        public static IEndpointRouteBuilder MapResetPasswordEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/resetpassword",async (ResetPasswordViewModle model,IMediator mediator) =>
                {
                    var result = await mediator.Send(
                        new ResetPasswordOrchestrator(
                            model.Email,
                            model.Token,
                            model.NewPassword,
                            model.ConfirmPassword
                        )
                    );

                    if (!result.IsSuccess)
                    {
                        return Results.Json(EndpointRespones<bool>.Fail(result.Message, result.StatusCode),
                            statusCode: result.StatusCode);
                    }

                    return Results.Json(EndpointRespones<bool>.Success(result.Data),
                        statusCode: result.StatusCode);

                });

            return app;
        }
    }
}
