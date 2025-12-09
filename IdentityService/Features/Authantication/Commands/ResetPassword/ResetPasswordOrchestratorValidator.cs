using FluentValidation;

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public class ResetPasswordOrchestratorValidator: AbstractValidator<ResetPasswordOrchestrator>
    {
        public ResetPasswordOrchestratorValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters.")
                .Matches(@"[A-Z]")
                    .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[0-9]")
                    .WithMessage("Password must contain at least one number.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}
