using FluentValidation;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.CategoryFeatures.Commands.CreateCategory
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
