using Domain_Layer.Respones;
using IdentityService.Features.User.Quries.GetuserbyEmail;
using IdentityService.Features.UserToken.Commands.RemoveUserToken;
using IdentityService.Features.UserToken.Quries;
using IdentityService.Features.UserToken.Quries.GetUserToken;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public record ResetPasswordOrchestrator(string Email,  string NewPassword, string ConfirmPassword) : IRequest<RequestRespones<bool>>;

    public class ResetPasswordOrchestratorHandler : IRequestHandler<ResetPasswordOrchestrator, RequestRespones<bool>>
    {
        private readonly IMediator mediator;

        public ResetPasswordOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<RequestRespones<bool>> Handle(ResetPasswordOrchestrator request, CancellationToken cancellationToken)
        {
            try
            {

                var GetUserRespone = await mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken);

                if (!GetUserRespone.IsSuccess || GetUserRespone.Data == null)
                {
                    return RequestRespones<bool>.Fail("User not found", 404);
                }

                var tokenResponse = await mediator.Send(new GetUserTokenByEmailQuery(request.Email));

                var validationResult = ValidateToken(tokenResponse.Data);
                if (!validationResult.IsSuccess)
                {
                    return validationResult;
                }


                var resetResult = await mediator.Send(new ResetPasswordCommend(GetUserRespone.Data.Id, request.NewPassword), cancellationToken);

                var RemoveUserTokenRespone = await mediator.Send(new RemoveUserTokenCommand(GetUserRespone.Data.Id), cancellationToken);

                return resetResult;

            }
            catch (Exception ex )
            {

                return RequestRespones<bool>.Fail("error while Reseeting password procces", 400);

            }

            


        }


        private RequestRespones<bool> ValidateToken(UserTokenToReturnDto userToken)
        {
            if (userToken == null)
            {
                return RequestRespones<bool>.Fail("Reset token not found", 400);
            }

            if (!userToken.IsVerified)
            {
                return RequestRespones<bool>.Fail(
                    "Verification required before resetting password",
                    403
                );
            }
            return RequestRespones<bool>.Success(true);
        }

    }

    
}
