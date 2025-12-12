using Domain_Layer.Respones;
using IdentityService.Features.UserToken.Quries.GetUserToken;
using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.UserToken.Quries
{
    public record GetUserTokenByEmailQuery(string UserEmail) :IRequest<RequestRespones<UserTokenToReturnDto>>;

    public class GetUserTokenQueryByEmailHandler : IRequestHandler<GetUserTokenByEmailQuery, RequestRespones<UserTokenToReturnDto>>
    {
        private readonly IGenericRepository<Shared.Entites.UserToken> genaricRepository;

        public GetUserTokenQueryByEmailHandler(IGenericRepository<Shared.Entites.UserToken> genaricRepository)
        {
            this.genaricRepository = genaricRepository;
        }
        public async Task<RequestRespones<UserTokenToReturnDto>> Handle(GetUserTokenByEmailQuery request, CancellationToken cancellationToken)
        {
            var userToken= await genaricRepository.GetQueryableByCriteria(ut => ut.User.Email == request.UserEmail)
                .OrderByDescending(x => x.ExpiredDate)
                .Select(x => new UserTokenToReturnDto
                {
                    id = x.Id,
                    Token = x.Token,
                    ExpiredDate = x.ExpiredDate,
                    UserId = x.UserId
                }).FirstOrDefaultAsync(cancellationToken); ;

            if (userToken==null)
            {
                return RequestRespones<UserTokenToReturnDto>.Fail("User token not found", 404);

            }

            return RequestRespones<UserTokenToReturnDto>.Success(userToken);

        }
    }




}
