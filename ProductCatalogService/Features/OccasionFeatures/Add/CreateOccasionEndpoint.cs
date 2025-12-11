using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Features.CategoryFeatures.Commands.UpdateCategory;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.OccasionFeatures.Add
{
     [ApiController]
    [Route("[controller]")]
    public class CreateOccasionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAddOccasionQr _add;

        public CreateOccasionController(IMediator mediator, IAddOccasionQr add)
        {
            _mediator = mediator;
            _add = add;
        }

        [HttpPost]
    //    [Route("api/occasions")]
        public async Task<ActionResult<Result<UpdateCategoryDTO>>> CreateOccasion([FromForm] OccasionRequest request1)
        {
            var result =await  _add.IAddOccasion(request1);
            return Ok(result);
        }

    }
}
