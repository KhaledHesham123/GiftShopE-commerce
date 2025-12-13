using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.OccasionFeatures.DeleteOccasion
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OccasionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OccasionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteOccasion([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteOccasionCommand(id));
            return StatusCode(result.StatusCode,result);
        }
    }
}
