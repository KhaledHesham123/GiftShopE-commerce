using MediatR;
using OrderService.Shared.Interface;
using OrderService.Shared.UIitofwork;
using System.Windows.Input;

namespace OrderService.Shared.Behavior
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IUnitofWork unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;

        public TransactionBehavior(
            IUnitofWork unitOfWork,
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
