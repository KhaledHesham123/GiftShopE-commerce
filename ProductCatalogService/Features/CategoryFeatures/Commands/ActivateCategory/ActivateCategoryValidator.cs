using FluentValidation;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.ActivateCategory
{
    public class ActivateCategoryValidator : AbstractValidator<ActivateCategoryCommand>
    {
        public ActivateCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category id is required.");
        }
    }
}
