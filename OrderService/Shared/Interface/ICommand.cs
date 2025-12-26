using MediatR;

namespace OrderService.Shared.Interface
{
    
        public interface ICommand<out TResponse> : IRequest<TResponse> { };
        public interface ICommand : IRequest<Unit> { };

}
