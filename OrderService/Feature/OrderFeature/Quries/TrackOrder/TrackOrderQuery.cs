using MassTransit.Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Repository;

namespace OrderService.Feature.OrderFeature.Quries.TrackOrder
{
    public record TrackOrderQuery(Guid Orderid):IRequest<RequestRespones<TrackOrderToreturn>>;

    public class TrackOrderQueryHanddler : IRequestHandler<TrackOrderQuery, RequestRespones<TrackOrderToreturn>>
    {
        private readonly IGenericRepository<Order> genericRepository;

        public TrackOrderQueryHanddler(IGenericRepository<Order> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<TrackOrderToreturn>> Handle(TrackOrderQuery request, CancellationToken cancellationToken)
        {

            var order = await  genericRepository.GetQueryableByCriteria(x=>x.Id==request.Orderid).Select(o=>new TrackOrderToreturn
            {
                OrderId=o.Id,
                OrderNumber=o.OrderNumber,
                Status=o.Status.ToString(),
                CurrentLat=o.CurrentLat,
                CurrentLng=o.CurrentLng,
                EstimatedArrivalTime=o.EstimatedArrivalTime,
                DeliveryHeroContact=o.DeliveryHeroContact,
                DeliveryHeroName=o.DeliveryHeroName,
                ShippingAddress=o.ShippingAddress,
                SubTotal=o.SubTotal,
                DeliveryFee=o.DeliveryFee,
                TotalAmount = o.SubTotal + o.DeliveryFee - o.PointsRedeemed,
                Items=o.OrderItems.Select(i=>new TrackOrderItemDto
                {
                    Price=i.Price,
                    ProductName=i.ProductName,
                    Quantity=i.Quantity
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);

            if (order==null)
            {
                return RequestRespones<TrackOrderToreturn>.Fail("there is no order with this id");
            }

            return RequestRespones<TrackOrderToreturn>.Success(order);


        }
    }
}
