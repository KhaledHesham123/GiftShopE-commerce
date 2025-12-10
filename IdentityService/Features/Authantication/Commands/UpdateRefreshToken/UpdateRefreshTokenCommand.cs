using IdentityService.Shared.Entites;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.UpdateRefreshToken
{
    public record UpdateRefreshTokenCommand(Shared.Entites.User User) : IRequest<bool>;
 
}
