using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserProfileService.Features.UserProfileFeature.Quries.GetUserByid
{
    [ApiController]
    [Route("api/Profile")] 
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetUserById")] 
        public async Task<IActionResult> GetUserById([FromQuery]Guid id)
        {
            var response = await _mediator.Send(new GetUserByidQuery(id));

            if (!response.IsSuccess)
            {
                var errorDetail = string.Join("| ", response.Errors?.Any() == true
                    ? response.Errors
                    : new[] { response.Message ?? "User not found" });

                return Problem(
                    detail: errorDetail,
                    statusCode: response.StatusCode
                );
            }

            return Ok(response);
        }
    }
}
