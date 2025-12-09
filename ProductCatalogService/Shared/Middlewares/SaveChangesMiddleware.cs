using ProductCatalogService.Data.DBContexts;

namespace ProductCatalogService.Shared.Middlewares
{
    public class SaveChangesMiddleware
    {
        private readonly RequestDelegate _next;
        public SaveChangesMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ProductCatalogDbContext _productCatalogDbContext)
        {
            await _next(context);

            if(context.Response.StatusCode is >= 200 and < 300)
            {
                if (_productCatalogDbContext.ChangeTracker.HasChanges())
                    await _productCatalogDbContext.SaveChangesAsync();
            }
            
        }
    }
}
