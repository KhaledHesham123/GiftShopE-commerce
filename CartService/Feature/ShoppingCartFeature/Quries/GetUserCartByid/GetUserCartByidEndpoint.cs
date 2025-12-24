using MediatR;

namespace CartService.Feature.ShoppingCartFeature.Quries.GetUserCartByid
{
    public static class GetUserCartByidEndpoint
    {
        public static IEndpointRouteBuilder MapGetUserCartByidEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id}/cart", async (string id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetUserCartByidQuery(id));

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
