using MediatR;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Interface;
using OrderService.Shared.Repository;

namespace OrderService.Feature.OrderIemsFeature.Command.AddOrderItems
{
    public record AddOrderItemsCommand(IEnumerable<OrderItemDto> OrderItems, Guid OrderId): IRequest<RequestRespones<bool>>;

    public class AddOrderItemsCommandHandler : IRequestHandler<AddOrderItemsCommand, RequestRespones<bool>>
    {
        private readonly IGenericRepository<OrderItem> genericRepository;

        public AddOrderItemsCommandHandler(IGenericRepository<OrderItem> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<bool>> Handle(AddOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var orderItems = request.OrderItems.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                ProductId = item.id,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity

            }).ToList();
            if (!orderItems.Any())
            {
                return RequestRespones<bool>.Fail("Error while adding OrderItems");
            }

            await genericRepository.AddRangeAsync(orderItems);

            await genericRepository.SaveChangesAsync();

            return RequestRespones<bool>.Success(true);

        }
    }
}
