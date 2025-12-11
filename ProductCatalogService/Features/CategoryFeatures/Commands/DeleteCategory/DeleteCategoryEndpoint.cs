using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.DeleteCategory
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // DELETE: /DeleteCategory/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<DeleteCategoryDTO>>> DeleteCategory([FromRoute] Guid id)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
