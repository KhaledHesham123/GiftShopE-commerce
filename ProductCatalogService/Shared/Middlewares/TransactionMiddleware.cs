using UserProfileService.Data.DBContexts;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Shared.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;
        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IUnitOfWork _unitOfWork)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _next(context);

                if (context.Response.StatusCode is >= 200 and < 300)
                {
                    //if (_productCatalogDbContext.ChangeTracker.HasChanges())
                        await _unitOfWork.CommitTransactionAsync();
                }
                else
                    await _unitOfWork.RollbackTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
