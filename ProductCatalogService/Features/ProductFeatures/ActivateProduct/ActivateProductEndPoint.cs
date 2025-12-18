using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.ActivateProduct
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IMediator _mediator) : ControllerBase
    {
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Result<ActivateProductDto>>> ActivateProduct([FromRoute] Guid id, [FromBody] ActivateProductCommand command)
        {
            // ensure route id and body id match if provided
            if (id != command.Id)
                return BadRequest(Result<ActivateProductDto>.FailResponse("Id mismatch.", new List<string> { "Id mismatch." }, 400));

            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
