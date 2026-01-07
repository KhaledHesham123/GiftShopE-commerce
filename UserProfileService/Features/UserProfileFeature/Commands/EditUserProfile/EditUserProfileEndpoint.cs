using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.Features.UserProfileFeature.DTOs;

namespace UserProfileService.Features.UserProfileFeature.Commands.EditUserProfile
{
    [ApiController]
    [Route("api/Profile")] 
    public class EditUserProfileEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public EditUserProfileEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Edit")] 
        public async Task<IActionResult> EditUserProfile([FromBody] EditUserProfileDto model)
        {
            var response = await _mediator.Send(new EditUserProfileCommand(
                model.userid,
                model.FirstName,
                model.LastName,
                model.Gender,
                model.ProfileImageUrl));

            if (!response.IsSuccess)
            {
                var errorDetail = string.Join("| ", response.Errors?.Any() == true
                    ? response.Errors
                    : new[] { response.Message ?? "Failed to update profile" });

                return Problem(
                    detail: errorDetail,
                    statusCode: response.StatusCode
                );
            }

            return Ok(response);
        }
    }
}
