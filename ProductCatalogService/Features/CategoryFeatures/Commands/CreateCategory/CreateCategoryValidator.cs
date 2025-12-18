using FluentValidation;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IRepository<Category> _repository;

        public CreateCategoryValidator(IRepository<Category> repository )
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100);

            RuleFor(x => x.IsActive)
                .NotNull();
        }
    }
}
