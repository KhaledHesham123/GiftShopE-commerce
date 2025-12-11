using FluentValidation;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.DeleteCategory
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category id is required.");
        }
    }
}
