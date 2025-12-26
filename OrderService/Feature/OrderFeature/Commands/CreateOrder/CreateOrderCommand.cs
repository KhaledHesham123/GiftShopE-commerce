using MediatR;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Interface;
using OrderService.Shared.Repository;

namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    public record CreateOrderCommand(Order Order): IRequest<RequestRespones<bool>>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, RequestRespones<bool>>
    {
        private readonly IGenericRepository<Order> genericRepository;

        public CreateOrderCommandHandler(IGenericRepository<Order> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<bool>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await genericRepository.AddAsync(request.Order);
                await genericRepository.SaveChangesAsync();
                return RequestRespones<bool>.Success(true);
            }
            catch (Exception ex)
            {

               return RequestRespones<bool>.Fail(string.Join(" | ", ex.Message, ex.InnerException?.Message),500);
            }
        }
    }



}
