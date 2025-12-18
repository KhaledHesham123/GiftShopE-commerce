using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.ActivateCategory
{
    [ApiController]
    [Route("[controller]")]
    public class ActivateCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivateCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PATCH: /ActivateCategory/{id}
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Result<ActivateCategoryDTO>>> ActivateCategory([FromRoute] Guid id, [FromBody] ActivateCategoryCommand command)
        {
            // ensure route id and body id match if provided
            if (id != command.Id)
                return BadRequest(Result<ActivateCategoryDTO>.FailResponse("Id mismatch.", new List<string> { "Id mismatch." }, 400));

            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
