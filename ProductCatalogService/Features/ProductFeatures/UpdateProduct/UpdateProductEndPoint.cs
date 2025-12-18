using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.UpdateProduct
{
    [ApiController]
    [Route("api/[controller]")]

    public class UpdateProductEndPoint(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Update")]
        public async Task<ActionResult<Result<string>>> UpdateProduct([FromBody] UpdateProductDto productDto)
        {
            var result = await _mediator.Send(new UpdateProductCommand(productDto.Id,
                                                                       productDto.Name,
                                                                       productDto.Description,
                                                                       productDto.Price,
                                                                       productDto.Stock,
                                                                       productDto.IsActive,
                                                                       productDto.CategoryId,
                                                                       productDto.OccasionIds,
                                                                       productDto.Tags,
                                                                       productDto.Images,
                                                                       productDto.Attributes));
             return StatusCode(result.StatusCode , result);
        }
    }
}
