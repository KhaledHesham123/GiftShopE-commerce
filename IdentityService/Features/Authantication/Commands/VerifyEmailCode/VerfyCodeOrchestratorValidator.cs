using FluentValidation;

namespace IdentityService.Features.Authantication.Commands.VerifyEmailCode
{
    public class VerfyCodeOrchestratorValidator: AbstractValidator<VerfyCodeOrchestrator>
    {
        public VerfyCodeOrchestratorValidator()
        {
            RuleFor(x => x.UserEmail)
              .NotEmpty().WithMessage("Email is required")
              .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.token)
                .NotEmpty().WithMessage("Verification code is required");
                
        }
    }
}
