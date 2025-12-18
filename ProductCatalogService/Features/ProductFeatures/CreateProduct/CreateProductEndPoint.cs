using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.CreateProduct
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var result = await _mediator.Send(new CreateProductCommand(productDto.Name,
                                                                     productDto.Description,
                                                                     productDto.Price,
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
