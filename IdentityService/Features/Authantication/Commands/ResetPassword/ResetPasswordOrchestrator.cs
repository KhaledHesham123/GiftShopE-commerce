using Domain_Layer.Respones;
using IdentityService.Features.User.Quries.GetuserbyEmail;
using IdentityService.Features.UserToken.Quries;
using IdentityService.Features.UserToken.Quries.GetUserToken;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public record ResetPasswordOrchestrator(string Email, string Token, string NewPassword, string ConfirmPassword) : IRequest<RequestRespones<bool>>;

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
                if (request.NewPassword != request.ConfirmPassword)
                    return RequestRespones<bool>.Fail("Passwords do not match", 400);

                var GetUserRespone = await mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken);

                if (!GetUserRespone.IsSuccess || GetUserRespone.Data == null)
                {
                    return RequestRespones<bool>.Fail("User not found", 404);
                }

                var GetUserTokenRespone = await mediator.Send(new GetUserTokenByEmailQuery(GetUserRespone.Data.Email), cancellationToken);

                var tokenValidationResult = ValidateToken(GetUserTokenRespone.Data, request.Token);

                if (!tokenValidationResult.IsSuccess)
                {
                    return tokenValidationResult;
                }

                return await mediator.Send(new ResetPasswordCommend(GetUserRespone.Data.Id, request.NewPassword), cancellationToken);

            }
            catch (Exception ex )
            {

                return RequestRespones<bool>.Fail("error while Reseeting password procces", 400);

            }

            


        }



        private RequestRespones<bool> ValidateToken(UserTokenToReturnDto tokenData, string providedToken)
        {
            if (tokenData == null)
            {
                return RequestRespones<bool>.Fail("Invalid or missing token record", 400);
            }

            if (tokenData.Token != providedToken)
            {
                return RequestRespones<bool>.Fail("Invalid token provided", 400);
            }

            //if (tokenData.ExpiredDate.ToUniversalTime() < DateTime.UtcNow)
            //{
            //    return RequestRespones<bool>.Fail("Token has expired", 400);
            //}

            return RequestRespones<bool>.Success(true);
        }
    }
}
