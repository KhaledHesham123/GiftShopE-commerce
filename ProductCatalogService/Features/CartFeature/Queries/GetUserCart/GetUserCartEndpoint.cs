using MediatR;

namespace ProductCatalogService.Features.CartFeature.Queries.GetUserCart
{
    public static class GetUserCartEndpoint
    {
        public static IEndpointRouteBuilder MapGetUserCartByidEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id}/cart\"", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetUserCartCommand(id));

                if (!response.Success)
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
