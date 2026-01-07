using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogService.Features.ProductFeatures.Commands.UpdateCartProductQuantity
{
    [Route("api/Product")]
    [ApiController]
    public class UpdateCartProductQuantityEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public UpdateCartProductQuantityEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("UpdateCartProductQuantity")]

        public async Task<ActionResult<bool>> UpdateCartProductQuantityCommand (UpdateProductDto modle)
        {
            var UpdateProductQuantityResult= await mediator.Send(new UpdateCartProductQuantityCommand(modle.Basketid, modle.newQuantity, modle.ProductId));

            if (UpdateProductQuantityResult.Success)
            {
                return Ok(UpdateProductQuantityResult);
            }

            return BadRequest(UpdateProductQuantityResult);

        }
    }

    public class UpdateProductDto
    {
        public Guid Basketid { get; set; }
        public Guid ProductId { get; set; }
        public int newQuantity { get; set; }
    }
}
