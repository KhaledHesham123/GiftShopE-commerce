using MediatR;

namespace UserProfileService.Features.UserProfileFeature.Quries.GetUserByid
{
    public static class GetUserByidEndpoint
    {
        public static IEndpointRouteBuilder MapGetUserByidEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/GetUserByid/{id:guid}", async (Guid id, IMediator mediator) =>
            {
            var response = await mediator.Send(
                new GetUserByidQuery(id));

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
