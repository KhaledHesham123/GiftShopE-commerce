using IdentityService.Shared.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Queries.GetUserByRefreshToken
{
    public record GetUserByRefreshTokenQuery(string refreshToken) : IRequest<ResponseResult<Shared.Entites.User>>;

}
