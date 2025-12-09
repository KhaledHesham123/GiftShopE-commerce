using Domain_Layer.Respones;
using IdentityService.Features.UserToken.Commands.CreateUserToken;
using IdentityService.Shared.Entites;
using IdentityService.Shared.Services.EmailVerificationServices;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    public record ForgetpasswordOrchestrator(string Email,string BaseUrl) : IRequest<RequestRespones<ForgotPasswordDTO>>;

    public class ForgetpasswordOrchestratorHandler:IRequestHandler<ForgetpasswordOrchestrator,RequestRespones<ForgotPasswordDTO>>
    {
        private readonly IMediator mediator;
        private readonly IEMailSettings mailSettings;

        public ForgetpasswordOrchestratorHandler(IMediator mediator, IEMailSettings mailSettings )
        {
            this.mediator = mediator;
            this.mailSettings = mailSettings;
        }
        public async Task<RequestRespones<ForgotPasswordDTO>> Handle(ForgetpasswordOrchestrator request, CancellationToken cancellationToken)
        {

            var userRespone = await mediator.Send(new Features.User.Quries.GetuserbyEmail.GetUserByEmailQuery(request.Email), cancellationToken);
            if (!userRespone.IsSuccess)
            {
                return RequestRespones<ForgotPasswordDTO>.Fail("there is no user with this Email", 404);
            }

            var code = GenerateVerificationCode();


            var userResetPasswordToken = await mediator.Send(new CreateUserTokenCommand(userRespone.Data.Id, code,10 ), cancellationToken);

            var resetUrl = $"{request.BaseUrl}/resetpassword-form?email={request.Email}&token={code}";
            var emial = new Email
            {
                subject = "Reset Your Password",
                body = resetUrl,
                to = request.Email
            };

            mailSettings.sendEmail(emial);  

            var emialDto = new ForgotPasswordDTO
            {
                Email = request.Email,
                ExpirationInMinutes = 10
            };

            return RequestRespones<ForgotPasswordDTO>.Success(emialDto);


        }
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }

    


}
