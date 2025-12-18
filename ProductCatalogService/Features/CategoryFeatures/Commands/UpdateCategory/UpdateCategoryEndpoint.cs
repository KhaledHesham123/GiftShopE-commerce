using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.UpdateCategory
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PUT: /UpdateCategory
        [HttpPut]
        public async Task<ActionResult<Result<UpdateCategoryDTO>>> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
