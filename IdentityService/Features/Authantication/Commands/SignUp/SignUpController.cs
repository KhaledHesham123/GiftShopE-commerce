using IdentityService.Shared.Respones;
using IdentityService.Shared.UIitofwork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Features.Authantication.Commands.SignUp
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SignUpController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofWork;
        public SignUpController(IMediator mediator , IUnitofWork unitofWork)
        {
            _mediator = mediator;
            _unitofWork = unitofWork;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult<string>>> SignUp([FromBody] SignUpDTO  signUpDTO)
        {
            var result = await _mediator.Send(new SignUpCommand(signUpDTO.FirstName , signUpDTO.LastName , signUpDTO.Email , signUpDTO.Password , signUpDTO.ConfirmPassword,signUpDTO.PhoneNumber , signUpDTO.Gender));
            await _unitofWork.SaveChangesAsync();
            return StatusCode(result.StatusCode, result);
        }
    }
}
