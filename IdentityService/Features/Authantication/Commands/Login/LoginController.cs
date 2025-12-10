using IdentityService.Shared.Respones;
using IdentityService.Shared.UIitofwork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityService.Features.Authantication.Commands.Login
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController(IMediator _mediator , IUnitofWork _unitofWork): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseResult<AuthModel>>> Login( LoginRequest  loginDto)
        {
            var result =await _mediator.Send(new LoginCommand(loginDto.Email , loginDto.Password));
            await _unitofWork.SaveChangesAsync();
            return StatusCode(result.StatusCode,result);
        }
    }
}
