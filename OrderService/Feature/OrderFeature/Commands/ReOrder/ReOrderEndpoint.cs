using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Feature.OrderFeature.Commands.CreateOrder;
using OrderService.Respones;

namespace OrderService.Feature.OrderFeature.Commands.ReOrder
{
    [Route("api/order")]
    [ApiController]
    public class ReOrderEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public ReOrderEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("ReOrder")] //api/order/ReOrder
        public async Task<ActionResult<EndpointRespones<OrderToReturnDto>>>ReOrder(ReOrderViewModle modle)
        {
            var result = await mediator.Send(new ReOrderOrchestrator(
                modle.OrderId,
                modle.NewShippingAddress,modle.Items));

            if (!result.IsSuccess || result.Data == null)
            {
                if (result.StatusCode == 404)
                    return NotFound(EndpointRespones<OrderToReturnDto>.Fail(result.Message ?? "not found"));

                return BadRequest(EndpointRespones<OrderToReturnDto>.Fail(result.Message ?? "Something went wrong"));
            }

            return Ok(EndpointRespones<OrderToReturnDto>.Success(result.Data));
        }
    }
}
