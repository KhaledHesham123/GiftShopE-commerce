using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Respones;

namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    [Route("api/Order")]
    [ApiController]
    public class CreateOrderEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public CreateOrderEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<EndpointRespones<OrderToReturnDto>>> CreateOrder(CreateOrderViewModel model)
        {
            var result = await mediator.Send( new CreateOrderOrchestrator(
                model.UserId,
                model.ShippingAddress,
                model.RecipientName,
                model.RecipientPhone, 
                model.PaymentMethod,
                model.PointsRedeemed,
                model.CurrentLat,
                model.CurrentLng));

            if (!result.IsSuccess)
            {
                if (result.StatusCode == 404)
                    return NotFound(EndpointRespones<OrderToReturnDto>.Fail(result.Message??"not found"));

                return BadRequest(EndpointRespones<OrderToReturnDto>.Fail(result.Message ?? "Something went wrong"));
            }
            return Ok(result.Data);
        }
    }
}
