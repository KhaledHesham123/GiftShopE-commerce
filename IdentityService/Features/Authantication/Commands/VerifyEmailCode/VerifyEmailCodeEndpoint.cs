using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Features.Authantication.Commands.VerifyEmailCode
{
    [ApiController]
    [Route("api/Identity")]
    public class VerifyEmailCodeEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public VerifyEmailCodeEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("verifyemailcode")]
        public async Task<IActionResult> VerifyEmailCode(VerifyEmailCodeDto modle )
        {
            var response = await _mediator.Send(new VerfyCodeOrchestrator(modle.email, modle.token));

            if (!response.IsSuccess)
            {
                return Problem(
                    detail: string.Join("; ", response.Errors.Any() ? response.Errors : new[] { response.Message ?? "" }),
                    statusCode: response.StatusCode
                );
            }

            return Ok(response);
        }
    }
}
