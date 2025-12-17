using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.DeleteProduct
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        [HttpDelete("{productId}")]
        public async Task<ActionResult<Result<string>>> DeleteProduct([FromRoute] Guid productId)
        {
            var result = await mediator.Send(new DeleteProductCommand(productId));          
            return StatusCode(result.StatusCode , result);
        }
    }
}
