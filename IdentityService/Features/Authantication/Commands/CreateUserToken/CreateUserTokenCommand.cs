namespace IdentityService.Features.Authantication.Commands.CreateUserToken
{
    public record CreateUserTokenCommand(Guid UserId,string token, int expiredInMin = 1);

}

