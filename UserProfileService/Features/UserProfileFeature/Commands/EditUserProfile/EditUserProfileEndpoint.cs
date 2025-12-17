using MediatR;
using UserProfileService.Features.UserProfileFeature.DTOs;

namespace UserProfileService.Features.UserProfileFeature.Commands.EditUserProfile
{
    public static class EditUserProfileEndpoint
    {
        public static IEndpointRouteBuilder MapEditUserProfileEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/users/Edit", async (EditUserProfileDto Modle, IMediator mediator) =>
            {
                var response = await mediator.Send(new EditUserProfileCommand(Modle.userid,Modle.FirstName,Modle.LastName,Modle.Gender,Modle.ProfileImageUrl));

                if (!response.IsSuccess)
                {
                    return Results.Problem(
                        detail: string.Join("| ", response.Errors.Any() ? response.Errors : new[] { response.Message ?? "" }),
                        statusCode: response.StatusCode
                    );
                }

                return Results.Ok(response);

            });
            return app;

        }
    }
}
