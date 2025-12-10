
using Domain_Layer.Respones;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IdentityService.Shared.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger )
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var messageString = string.Join(" | ", errorMessages);

                
                var response = RequestRespones<string>.Fail(message:messageString,statuscode:400);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "System Error"); 

                var response = RequestRespones<string>.Fail(message: "Something went wrong",statuscode:500);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
