using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Feature.ShoppingCartFeature.Quries.GetUserCartByid
{
    [Route("api/Cart")]
    [ApiController]
    public  class GetUserCartByidEndpoint: ControllerBase
    {
        
            private readonly IMediator _mediator;

            public GetUserCartByidEndpoint(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet("getUserCartById")]
            public async Task<IActionResult> GetUserCartById([FromQuery] string id)
            {
                var response = await _mediator.Send(new GetUserCartByidQuery(id));

                if (!response.IsSuccess)
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