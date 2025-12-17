using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Shared.Middlewares
{
    public class SaveChangesMiddleware
    {
        private readonly RequestDelegate _next;

        public SaveChangesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork _unitOfWork)
        {
            await _next(context);

            if (context.Response.StatusCode is >= 200 and < 300)
            {
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
