using IdentityService.Shared.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.SignUp
{
    public record SignUpCommand(string FirstName , string LastName , string Email , string Password, string ConfirmPassword, string PhoneNumber , string Gender) : IRequest<ResponseResult<string>>;

}
