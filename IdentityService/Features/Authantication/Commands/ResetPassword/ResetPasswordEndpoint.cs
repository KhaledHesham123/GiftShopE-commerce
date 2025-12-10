using Azure;
using Domain_Layer.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public static class ResetPasswordEndpoint
    {
        public static IEndpointRouteBuilder MapResetPasswordEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/resetpassword", async (ResetPasswordViewModle model, IMediator mediator) =>
                {
                    var response = await mediator.Send(
                        new ResetPasswordOrchestrator(
                            model.Email,
                            model.Token,
                            model.NewPassword,
                            model.ConfirmPassword
                        )
                    );

                    if (!response.IsSuccess)
                    {
                        return Results.Problem(
                            detail: string.Join("| ", response.Errors.Any() ? response.Errors : new[] { response.Message ?? "" }),
                            statusCode: response.StatusCode
                        );
                    }

                    return Results.Ok(response);

                });
            return app;
            
        }
    
    }
}


