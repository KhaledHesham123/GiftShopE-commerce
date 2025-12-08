using FluentValidation;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Shared.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                //_logger.LogWarning("Validation error: {Errors}", ex.Errors);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = "Validation failed",
                    errors
                });
            }
            catch (Exception ex) // other exception
            {
                //_logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Something went wrong. Please try again later."
                });
            }
        }
    }
}