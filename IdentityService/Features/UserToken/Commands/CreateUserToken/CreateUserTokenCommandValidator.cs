using FluentValidation;

namespace IdentityService.Features.UserToken.Commands.CreateUserToken
{
    public class CreateUserTokenCommandValidator
       : AbstractValidator<CreateUserTokenCommand>
    {
        public CreateUserTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.token)
                .NotEmpty().WithMessage("Token is required.")
                .MinimumLength(6).WithMessage("Token must be at least 6 characters.")
                .MaximumLength(128).WithMessage("Token must not exceed 128 characters.");

            RuleFor(x => x.expiredInMin)
                .GreaterThan(0).WithMessage("Expiration time must be greater than 0 minutes.")
                .LessThanOrEqualTo(1440).WithMessage("Expiration time must not exceed 24 hours.");
        }
    }
}
