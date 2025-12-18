using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.CreateCategory
{
    [ApiController]
    [Route("[controller]")]
    public class CreateCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreateCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<Result<CreateCategoryDTO>>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
