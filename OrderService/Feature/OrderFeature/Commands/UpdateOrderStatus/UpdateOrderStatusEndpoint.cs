using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Feature.OrderFeature.Commands.CreateOrder;
using OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus.DTOs;
using OrderService.Respones;

namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus
{
    [Route("api/Order")]
    [ApiController]
    public class UpdateOrderStatusEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public UpdateOrderStatusEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("UpdateOrderStatus")]
        public async Task<ActionResult<EndpointRespones<OrderTrackingToReturnDto>>> UpdateOrderStatus(UpdateOrderStatusViewModle modle)
        {
           var Result = await mediator.Send(new UpdateOrderStatusOrchestrator(
               modle.OrderId,
               modle.Status,modle.DeliveryHeroName
               ,modle.DeliveryHeroContact
               ,modle.CurrentLat,
               modle.CurrentLng,
               modle.EstimatedArrivalTime));

            if (!Result.IsSuccess||Result.Data==null)
            {

                if (Result.StatusCode == 404)
                    return NotFound(EndpointRespones<OrderToReturnDto>.Fail(Result.Message ?? "not found"));

                return BadRequest(EndpointRespones<OrderToReturnDto>.Fail(Result.Message ?? "Something went wrong"));

            }

            return Ok(EndpointRespones<OrderTrackingToReturnDto>.Success(Result.Data));
        }
    }
}
