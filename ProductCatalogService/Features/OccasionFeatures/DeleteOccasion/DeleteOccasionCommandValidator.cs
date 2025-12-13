using FluentValidation;

namespace ProductCatalogService.Features.OccasionFeatures.DeleteOccasion
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
