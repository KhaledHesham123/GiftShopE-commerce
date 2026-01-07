using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogService.Features.CartFeature.Queries.GetUserCart
{

    [ApiController]
    [Route("api/users")]
    public class GetUserCartEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetUserCartEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getcartbyid")]
        public async Task<IActionResult> GetUserCart([FromQuery] Guid id)
        {
            var response = await _mediator.Send(new GetUserCartCommand(id));

            if (!response.Success)
            {
                var errorDetail = string.Join("| ", response.Errors.Any()
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
