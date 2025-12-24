using CartService.Respones;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Feature.ShoppingCartFeature.Commands.Updateproductquantity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateProductQuantityEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public UpdateProductQuantityEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<EndpointRespones<bool>>> updateProductQuantityInUserBasket(UpdateProductDto modle)  
        {
            var UpdateProductQuantityResult = await mediator.Send(new UpdateProductQuantityCommand(modle.Basketid, modle.newQuantity, modle.ProductId));
            if (UpdateProductQuantityResult.IsSuccess)
            {
                return EndpointRespones<bool>.Success(true);
            }
            return EndpointRespones<bool>.Fail(UpdateProductQuantityResult.Message??"some thing went wronge");
        }
    }
    public class UpdateProductDto {
        public Guid Basketid { get; set; }
        public Guid ProductId { get; set; }
        public int newQuantity { get; set; }
    }
}
