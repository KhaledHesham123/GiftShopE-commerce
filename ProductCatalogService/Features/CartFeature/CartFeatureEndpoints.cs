using ProductCatalogService.Features.CartFeature.Queries.GetUserCart;
using ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart;

namespace ProductCatalogService.Features.CartFeature
{
    public static class CartFeatureEndpoints
    {
        public static IEndpointRouteBuilder MapCartFeatureEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetUserCartByidEndpoint();

            return app;
        }
    }
}
