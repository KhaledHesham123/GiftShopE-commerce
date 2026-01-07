using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart
{
    [ApiController]
    [Route("api/Product")] 
    public class AddProductToCartEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddProductToCartEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddToCart")] 
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartDTo model)
        {
            var response = await _mediator.Send(new AddProductToCartCommand(
                model.userid,
                model.ProductId,
                model.Quantity));

            if (!response.Success)
            {
                var errorDetail = string.Join("| ", response.Errors?.Any() == true
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
