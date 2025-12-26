using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Respones;

namespace ProductCatalogService.Features.ProductFeatures.Queries.GetProductsByids
{
    [Route("api/Products")]
    [ApiController]
    public class GetProductsByidsEndpoint : ControllerBase
    {
        private readonly IMediator mediator;

        public GetProductsByidsEndpoint(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("GetProductsByIds")]
        public async Task<ActionResult<EndpointRespones<IEnumerable<ProductTOReturnDto>>>> GetProductsByIds( IEnumerable<Guid> productIds)
        {
            var result = await mediator.Send(new GetProductsByidsQuery(productIds));

            if (!result.Success || result.Data == null)
            {
                if (result.StatusCode == 404)
                    return NotFound(EndpointRespones<IEnumerable<ProductTOReturnDto>>.Fail(result.Message ?? "not found"));
                return BadRequest(EndpointRespones<IEnumerable<ProductTOReturnDto>>.Fail(result.Message ?? "Something went wrong"));
            }
            return Ok(EndpointRespones<IEnumerable<ProductTOReturnDto>>.Success(result.Data));
        }
    }
}
