using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart;

namespace ProductCatalogService.Features.CategoryFeatures.Queries.ExploreCategories
{
    [ApiController]
    [Route("api/Categories")]
    public  class GetAllCategoriesEndpoint: ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllCategoriesEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")] 
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllCategoriesQuery());

            if (!response.Success)
            {
                return Problem(
                    detail: string.Join(" | ", response.Errors?.Any() == true
                        ? response.Errors
                        : new[] { response.Message ?? "An error occurred" }),
                    statusCode: response.StatusCode
                );
            }

            return Ok(response);
        }
    }
}
}
