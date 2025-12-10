using MediatR;

namespace ProductCatalogService.Features.Occasion.Commands.UpdateOccasion
{
    public static class UpdateOccasionEndpoint
    {
        public static IEndpointRouteBuilder MapUpdateOccasionEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/forgetpassword", async (UpdateOccasionModle modle, IMediator mediator, HttpRequest request) =>
            {
               

                var response = await mediator.Send(new UpdateOccasionCommand(modle.id,modle.Name,modle.Status));

                if (!response.Success)
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
