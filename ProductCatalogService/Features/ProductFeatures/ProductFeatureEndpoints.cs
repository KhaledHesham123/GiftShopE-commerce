using ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart;

namespace ProductCatalogService.Features.ProductFeatures
{
    public static class ProductFeatureEndpoints
    {
        public static IEndpointRouteBuilder MapProductFeatureEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapAddProductToCartEndpoint();

            return app;
        }
    }
}
