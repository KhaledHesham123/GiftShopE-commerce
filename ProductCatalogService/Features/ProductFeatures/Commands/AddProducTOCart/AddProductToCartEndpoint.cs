using MediatR;

namespace ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart
{
    public static class AddProductToCartEndpoint
    {
        public static IEndpointRouteBuilder MapAddProductToCartEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/users/Edit", async (AddProductToCartDTo Modle, IMediator mediator) =>
            {
                var response = await mediator.Send(new AddProductToCartCommand(Modle.userid,Modle.ProductId,Modle.Quantity));

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
