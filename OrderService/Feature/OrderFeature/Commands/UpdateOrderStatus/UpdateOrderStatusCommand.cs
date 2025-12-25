using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus.DTOs;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Repository;

namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus
{
    public record UpdateOrderStatusCommand(Guid orderid, OrderStatus orderStatus, string? DeliveryHeroName, string? DeliveryHeroContact,
          double? CurrentLat, double? CurrentLng, DateTime? EstimatedArrivalTime) : IRequest<RequestRespones<OrderTrackingToReturnDto>>;

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, RequestRespones<OrderTrackingToReturnDto>>
    {
        private readonly IGenericRepository<Order> genericRepository;

        public UpdateOrderStatusCommandHandler(IGenericRepository<Order> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<OrderTrackingToReturnDto>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await genericRepository.GetQueryableByCriteria(o => o.Id == request.orderid).FirstOrDefaultAsync(cancellationToken);

            if (order==null)
            {
                return RequestRespones<OrderTrackingToReturnDto>.Fail("there is no order with this id");
            }

            if (request.orderStatus == OrderStatus.Cancelled && order.Status == OrderStatus.Delivered)
            {
                return RequestRespones<OrderTrackingToReturnDto>.Fail("Cannot cancel an order that has already been delivered.");
            }

            order.Status = request.orderStatus;
            order.DeliveryHeroName = request.DeliveryHeroName;
            order.DeliveryHeroContact = request.DeliveryHeroContact;
            order.CurrentLat = request.CurrentLat;
            order.CurrentLng = request.CurrentLng;
            order.EstimatedArrivalTime = request.EstimatedArrivalTime;

            genericRepository.SaveInclude(order);

            await genericRepository.SaveChangesAsync();

            

            var mapppedOrder = new OrderTrackingToReturnDto
            {
                CurrentStatus = order.Status,
                OrderNumber = order.OrderNumber,
                DeliveryLocation = new DeliveryLocationDto
                {
                    EstimatedArrival = order.EstimatedArrivalTime,
                    HeroName = order.DeliveryHeroName,
                    HeroContact = order.DeliveryHeroContact,
                    Lat = order.CurrentLat,
                    Lng = order.CurrentLng
                },
            };
            return RequestRespones<OrderTrackingToReturnDto>.Success(mapppedOrder);


        }
    }
}
