using MediatR;
using OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus.DTOs;
using OrderService.Feature.OrderStatusLogFeature.Commands.UpdateOrderStatusLog;
using OrderService.Respones;
using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus
{
    public record UpdateOrderStatusOrchestrator(Guid orderid, OrderStatus orderStatus, string? DeliveryHeroName, string? DeliveryHeroContact,
          double? CurrentLat, double? CurrentLng, DateTime? EstimatedArrivalTime) : IRequest<RequestRespones<OrderTrackingToReturnDto>>;

    public class UpdateOrderStatusOrchestratorHandler : IRequestHandler<UpdateOrderStatusOrchestrator, RequestRespones<OrderTrackingToReturnDto>>
    {
        private readonly IMediator mediator;

        public UpdateOrderStatusOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<RequestRespones<OrderTrackingToReturnDto>> Handle(UpdateOrderStatusOrchestrator request, CancellationToken cancellationToken)
        {
            var UpdateOrderStatusResult = await mediator.Send(new UpdateOrderStatusCommand(
                request.orderid,
                request.orderStatus,
                request.DeliveryHeroName,
                request.DeliveryHeroContact,
                request.CurrentLat,
                request.CurrentLng,
                request.EstimatedArrivalTime
                ), cancellationToken);

            if (!UpdateOrderStatusResult.IsSuccess|| UpdateOrderStatusResult.Data==null)
            {
                return RequestRespones<OrderTrackingToReturnDto>.Fail(UpdateOrderStatusResult.Message ?? "Error wihle Updating order status", UpdateOrderStatusResult.StatusCode);

            }

            var AddOrderStatuslogResult = await mediator.Send(new AddOrderStatusLogCommand(request.orderid, request.orderStatus, DateTime.UtcNow));
            if (!AddOrderStatuslogResult.IsSuccess|| AddOrderStatuslogResult.Data == null)
            {
                return RequestRespones<OrderTrackingToReturnDto>.Fail(AddOrderStatuslogResult.Message ?? "Error wihle adding order status log", AddOrderStatuslogResult.StatusCode);
            }
            UpdateOrderStatusResult.Data.StatusHistory.Add(AddOrderStatuslogResult.Data);

            return RequestRespones<OrderTrackingToReturnDto>.Success(UpdateOrderStatusResult.Data);

        }
    }
}
