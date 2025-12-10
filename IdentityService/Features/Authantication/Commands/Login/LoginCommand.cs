using IdentityService.Shared.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ResponseResult<AuthModel>>;
}
