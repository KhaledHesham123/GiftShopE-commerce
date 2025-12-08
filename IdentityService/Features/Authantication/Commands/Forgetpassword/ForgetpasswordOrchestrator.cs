using Domain_Layer.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    public record ForgetpasswordOrchestrator(string Email) : IRequest<RequestRespones<ForgotPasswordDTO>>;

    public class ForgetpasswordOrchestratorHandler:IRequestHandler<ForgetpasswordOrchestrator,RequestRespones<ForgotPasswordDTO>>
    {
        private readonly IMediator mediator;

        public ForgetpasswordOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<RequestRespones<ForgotPasswordDTO>> Handle(ForgetpasswordOrchestrator request, CancellationToken cancellationToken)
        {

            var userRespone = await mediator.Send(new Features.User.Quries.GetuserbyEmail.GetUserByEmailQuery(request.Email), cancellationToken);
            if (!userRespone.IsSuccess)
            {
                return RequestRespones<ForgotPasswordDTO>.Fail("there is no user with this Email", 404);
            }

            return RequestRespones<ForgotPasswordDTO>.Fail("there is no user with this Email", 404);


        }
    }


}
