using FluentValidation;

namespace IdentityService.Features.User.Quries.GetuserbyEmail
{
    public class GetuserbyEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetuserbyEmailQueryValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");
        }
    }
}
