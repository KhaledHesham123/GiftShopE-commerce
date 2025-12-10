using FluentValidation;

namespace ProductCatalogService.Features.OccasionFeatures.Commands.UpdateOccasion
{
    public class UpdateOccasionCommandValidator:AbstractValidator<UpdateOccasionCommand>
    {
        public UpdateOccasionCommandValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Occasion name is required.")

                .MaximumLength(100)
                .WithMessage("Occasion name cannot exceed 100 characters.");

                
        }
    }
}
