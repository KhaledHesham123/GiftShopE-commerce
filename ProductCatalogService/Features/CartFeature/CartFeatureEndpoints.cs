using ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart;

namespace ProductCatalogService.Features.CartFeature
{
    public static class CartFeatureEndpoints
    {
        public static IEndpointRouteBuilder MapCartFeatureEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapAddProductToCartEndpoint();

            return app;
        }
    }
}
