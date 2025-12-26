using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Interface;
using OrderService.Shared.Repository;

namespace OrderService.Feature.OrderStatusLogFeature.Commands.UpdateOrderStatusLog
{
    public record AddOrderStatusLogCommand(Guid OrderId, OrderStatus Status, DateTime ChangedAt) : ICommand<RequestRespones<OrderStatusLogDto>>;

    public class AddOrderStatusLogCommandHandler : IRequestHandler<AddOrderStatusLogCommand, RequestRespones<OrderStatusLogDto>>
    {
        private readonly IGenericRepository<OrderStatusLog> genericRepository;

        public AddOrderStatusLogCommandHandler(IGenericRepository<OrderStatusLog> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<OrderStatusLogDto>> Handle(AddOrderStatusLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newLog = new OrderStatusLog
                {
                    OrderId = request.OrderId,
                    Status = request.Status,
                    ChangedAt = request.ChangedAt
                };

                await genericRepository.AddAsync(newLog);

                var result = await genericRepository.SaveChangesAsync();

                if (result <= 0)
                {
                    return RequestRespones<OrderStatusLogDto>.Fail("Failed to log status change");
                }

                var logDto = new OrderStatusLogDto
                {
                    Status = newLog.Status,
                    ChangedAt = newLog.ChangedAt
                };

                return RequestRespones<OrderStatusLogDto>.Success(logDto);
            }
            catch (Exception ex)
            {

                return RequestRespones<OrderStatusLogDto>.Fail($"Failed to log status change{string.Join("|",ex.Message)}");

            }

        }
    }



}
