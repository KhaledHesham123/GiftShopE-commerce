using MediatR;
using ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart;

namespace ProductCatalogService.Features.CategoryFeatures.Queries.ExploreCategories
{
    public static class GetAllCategoriesEndpoint
    {
        public static IEndpointRouteBuilder MapGetAllCategoriesEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/Categoy/GetAll", async ( IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllCategoriesQuery());

                if (!response.Success)
                {
                    return Results.Problem(
                        detail: string.Join(" | ", response.Errors.Any() ? response.Errors : new[] { response.Message ?? "" }),
                        statusCode: response.StatusCode
                    );
                }

                return Results.Ok(response);

            });
            return app;

        }
    }
}
