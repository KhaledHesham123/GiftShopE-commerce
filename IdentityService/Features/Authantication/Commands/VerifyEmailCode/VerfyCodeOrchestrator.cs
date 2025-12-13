using Domain_Layer.Respones;
using IdentityService.Features.User.Quries.GetuserbyEmail;
using IdentityService.Features.UserToken.Quries;
using IdentityService.Features.UserToken.Quries.GetUserToken;
using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.VerifyEmailCode
{
    public record VerfyCodeOrchestrator(string UserEmail,string token):IRequest<RequestRespones<bool>>;

    public class VerfyCodeOrchestratorHandler : IRequestHandler<VerfyCodeOrchestrator, RequestRespones<bool>>
    {
        private readonly IMediator mediator;
        private readonly IGenericRepository<Shared.Entites.UserToken> genericRepository;

        public VerfyCodeOrchestratorHandler(IMediator mediator,IGenericRepository<Shared.Entites.UserToken> genericRepository)
        {
            this.mediator = mediator;
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<bool>> Handle(VerfyCodeOrchestrator request, CancellationToken cancellationToken)
        {

            var GetUserRespone = await mediator.Send(new GetUserByEmailQuery(request.UserEmail), cancellationToken);

            if (!GetUserRespone.IsSuccess || GetUserRespone.Data == null)
            {
                return RequestRespones<bool>.Fail("User not found", 404);
            }

            var GetUserTokenRespone = await mediator.Send(new GetUserTokenByEmailQuery(request.UserEmail), cancellationToken);

            var tokenValidationResult = ValidateToken(GetUserTokenRespone.Data, request.token);


            if (!tokenValidationResult.IsSuccess)
            {
                return tokenValidationResult;
            }

            else 
            {
               var Token= GetUserTokenRespone.Data;
                Token.IsVerified = true;
                Token.VerifiedAt = DateTime.UtcNow;

                genericRepository.SaveInclude(new Shared.Entites.UserToken 
                { 
                    Id=Token.id,
                    UserId=Token.UserId,
                    Token=Token.Token,
                    ExpiredDate=Token.ExpiredDate,
                    IsVerified=Token.IsVerified,
                    VerifiedAt=Token.VerifiedAt
                });
                await genericRepository.SaveChangesAsync();
            }





            return RequestRespones<bool>.Success(true);

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
