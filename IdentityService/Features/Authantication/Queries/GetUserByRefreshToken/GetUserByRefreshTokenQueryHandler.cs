using IdentityService.Shared.Repository;
using IdentityService.Shared.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Queries.GetUserByRefreshToken
{
    public class GetUserByRefreshTokenQueryHandler(IGenericRepository<Shared.Entites.User> _userRepository) : IRequestHandler<GetUserByRefreshTokenQuery, ResponseResult<Shared.Entites.User>>
    {
        public async Task<ResponseResult<Shared.Entites.User>> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByCriteriaAsync(u => u.UserTokens.Any(ut => ut.Token == request.refreshToken));
            if (user is null)
                return ResponseResult<Shared.Entites.User>.FailResponse("User not found with the provided refresh token.", statusCode: 404);
            return ResponseResult<Shared.Entites.User>.SuccessResponse(user, "User retrieved successfully.");
        }
    }
}
