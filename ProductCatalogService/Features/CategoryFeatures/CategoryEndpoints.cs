using ProductCatalogService.Features.CategoryFeatures.Queries.ExploreCategories;

namespace ProductCatalogService.Features.CategoryFeatures
{
    public static class CategoryEndpoints
    {
        public static IEndpointRouteBuilder mapCategoryEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapGetAllCategoriesEndpoint();
            return app;
        }
    }
}
