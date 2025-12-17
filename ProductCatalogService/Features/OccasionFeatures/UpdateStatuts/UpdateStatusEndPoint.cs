using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.Features.CategoryFeatures.Commands.UpdateCategory;
using UserProfileService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using UserProfileService.Features.Shared;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.OccasionFeatures.UpdateStatuts
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateStatusController : Controller
    {
        private readonly IMediator _mediator;

        public UpdateStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{Id:guid}")]
        public async Task<ActionResult<Result<UpdateCategoryDTO>>> UpdateStatus([FromQuery]Guid Id, bool Status )
        {
            var result = await _mediator.Send( new UpdateStatus(Id, Status));
            return Ok(result);
        }
    }
}
