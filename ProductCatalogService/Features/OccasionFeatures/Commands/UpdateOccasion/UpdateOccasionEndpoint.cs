using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogService.Features.OccasionFeatures.Commands.UpdateOccasion
{

    [ApiController]
    [Route("api/Occasion")] 
    public class UpdateOccasionEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateOccasionEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("updateOccasion")] 
        public async Task<IActionResult> UpdateOccasion([FromBody] UpdateOccasionModle model)
        {
            var response = await _mediator.Send(new UpdateOccasionCommand(model.id, model.Name, model.Status));

            if (!response.Success)
            {
                var errorDetail = string.Join("; ", response.Errors?.Any() == true
                    ? response.Errors
                    : new[] { response.Message ?? "An error occurred" });

                return Problem(
                    detail: errorDetail,
                    statusCode: response.StatusCode
                );
            }

            return Ok(response);
        }
    }
}
    
