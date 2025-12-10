using FluentValidation;
using IdentityService.Shared.Enums;

namespace IdentityService.Features.Authantication.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator() 
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain a number");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords must match");
            RuleFor(x => x.PhoneNumber)
                 .NotEmpty()
                 .Matches(@"^(?:\+201[0-2,5][0-9]{8}|01[0-2,5][0-9]{8})$")
                 .WithMessage("Invalid phone number format");
            RuleFor(x => x.Gender).NotEmpty().Must(g => Enum.TryParse<Gender>(g , true , out _)).WithMessage("Invalid gender");

        }     
    }
}
