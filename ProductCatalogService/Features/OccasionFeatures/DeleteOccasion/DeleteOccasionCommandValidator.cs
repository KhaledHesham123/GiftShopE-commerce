using FluentValidation;

namespace UserProfileService.Features.OccasionFeatures.DeleteOccasion
{
    public class DeleteOccasionCommandValidator : AbstractValidator<DeleteOccasionCommand>
    {
        public DeleteOccasionCommandValidator()
        {
            RuleFor(x => x.occasionId)
            .NotEmpty().WithMessage("Occasion ID is required.");
        }
    }
}
