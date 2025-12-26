using MassTransit.Mediator;
using MediatR;
using OrderService.Respones;

namespace OrderService.Feature.OrderFeature.Quries.TrackOrder
{
    public record TrackOrderQuery:IRequest<RequestRespones<TrackOrderToreturn>>;

    public class TrackOrderQueryHanddler : IRequestHandler<TrackOrderQuery, RequestRespones<TrackOrderToreturn>
{
        public Task<RequestRespones<TrackOrderToreturn>> Handle(TrackOrderQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
