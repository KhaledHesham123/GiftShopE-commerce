using IdentityService.Shared.UIitofwork;
using MediatR;
using System.Windows.Input;

namespace IdentityService.Shared.Behavior
{
    public class TransactionBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>where TRequest : ICommand
    {
        private readonly IunitofWork unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;

        public TransactionBehavior(
            IunitofWork unitOfWork,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

                var response = await next();

                await unitOfWork.CommitTransactionAsync();

                return response;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                logger.LogError(ex, "Transaction failed for {Request}", typeof(TRequest).Name);
                throw;
            }
        }
    }
}
