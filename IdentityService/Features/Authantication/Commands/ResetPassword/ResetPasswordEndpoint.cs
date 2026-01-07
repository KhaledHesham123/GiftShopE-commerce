using Azure;
using Domain_Layer.Respones;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    [ApiController]
    [Route("api/Identity")]
    public  class ResetPasswordEndpoint: ControllerBase
    {
       
       
            private readonly IMediator _mediator;

            public ResetPasswordEndpoint(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("reset-password")]
            public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModle model)
            {
                var response = await _mediator.Send(
                    new ResetPasswordOrchestrator(
                        model.Email,
                        model.NewPassword,
                        model.ConfirmPassword
                    )
                );

                if (!response.IsSuccess)
                {
                    return Problem(
                        detail: string.Join("| ", response.Errors.Any() ? response.Errors : new[] { response.Message ?? "" }),
                        statusCode: response.StatusCode
                    );
                }

                return Ok(response);
            }
        }

    }



