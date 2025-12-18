using FluentValidation;

namespace UserProfileService.Features.UserProfileFeature.Commands.EditUserProfile
{
    public class EditUserProfileCommandValidator:AbstractValidator<EditUserProfileCommand>
    {
        public EditUserProfileCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty()
                .WithMessage("UserId is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .Must(g => g == "Male" || g == "Female")
                .WithMessage("Gender must be Male or Female");

            RuleFor(x => x.ProfileImageUrl)
                .NotEmpty()
                .WithMessage("Profile image URL is invalid");
        }
    }
}
