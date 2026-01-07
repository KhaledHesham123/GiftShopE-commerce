using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Respones;

namespace OrderService.Feature.OrderFeature.Quries.TrackOrder
{
    [Route("api/Order")]
    [ApiController]
    public class TrackOrderEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public TrackOrderEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("TrackOrder")] //api/order/TrackOrder
        public async Task<ActionResult<EndpointRespones<TrackOrderToreturn>>> TrackOrder([FromQuery] Guid orderId)
        {
            var result = await mediator.Send(new TrackOrderQuery(orderId));
            if (!result.IsSuccess||result.Data==null)
            {
                if (result.StatusCode == 404)
                    return NotFound(EndpointRespones<TrackOrderToreturn>.Fail(result.Message ?? "not found"));
                return BadRequest(EndpointRespones<TrackOrderToreturn>.Fail(result.Message ?? "Something went wrong"));
            }
            return Ok(EndpointRespones<TrackOrderToreturn>.Success(result.Data));
        }
    }
}
