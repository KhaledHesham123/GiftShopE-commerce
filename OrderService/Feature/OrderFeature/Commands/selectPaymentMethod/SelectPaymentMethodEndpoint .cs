using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Feature.OrderFeature.Commands.CreateOrder;
using OrderService.Respones;

namespace OrderService.Feature.OrderFeature.Commands.selectPaymentMethod
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectPaymentMethodEndpoint   : ControllerBase
    {
        private readonly IMediator mediator;

        public SelectPaymentMethodEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost] // POST api/order/SelectPaymentMethod
        public async Task<ActionResult<EndpointRespones<OrderToReturnDto>>> SelectPaymentMethod([FromBody]selectPaymentMethodViewModle modle) 
        {
            var result = await mediator.Send(new selectPaymentMethodCommand(modle.orderid, modle.PaymentMethods));
            if (!result.IsSuccess || result.Data == null)
            {
                return result.StatusCode == 404
                    ? NotFound(EndpointRespones<OrderToReturnDto>.Fail(result.Message))
                    : BadRequest(EndpointRespones<OrderToReturnDto>.Fail(result.Message ?? "Something went wrong"));
            }

            return Ok(EndpointRespones<OrderToReturnDto>.Success(result.Data));
        }
    }
}
