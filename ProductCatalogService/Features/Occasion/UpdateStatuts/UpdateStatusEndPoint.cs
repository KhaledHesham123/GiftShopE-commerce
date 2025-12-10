using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.CategoryFeatures.Commands.UpdateCategory;
using ProductCatalogService.Features.Occasion.Add.AddOccasion.Dto;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.Occasion.UpdateStatuts
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
