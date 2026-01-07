using IdentityService.Features.Authantication.Commands.Login;
using IdentityService.Shared.Services.EmailVerificationServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    [ApiController]
    [Route("api/Identity")]
    public  class ForgetpasswordEndpoint: ControllerBase
    {
            private readonly IMediator _mediator;

            public ForgetpasswordEndpoint(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("forgetpassword")]
            public async Task<IActionResult> ForgetPassword([FromQuery] string email)
            {
                var scheme = Request.Scheme;
                var host = Request.Host.Value;
                var baseUrl = $"{scheme}://{host}";

                var response = await _mediator.Send(new ForgetpasswordOrchestrator(email, baseUrl));

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
