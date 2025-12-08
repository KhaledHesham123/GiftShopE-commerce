using FluentValidation;

namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    public class ForgetpasswordOrchestratorValidator:AbstractValidator<ForgetpasswordOrchestrator>
    {
        public ForgetpasswordOrchestratorValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().EmailAddress()
               .WithMessage("Invalid email address");
        }
    }
}
