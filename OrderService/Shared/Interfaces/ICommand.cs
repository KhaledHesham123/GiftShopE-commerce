using MediatR;

namespace OrderService.Shared.Interfaces
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}


