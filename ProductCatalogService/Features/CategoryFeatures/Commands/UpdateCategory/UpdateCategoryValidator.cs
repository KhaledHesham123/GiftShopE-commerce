using FluentValidation;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.CategoryFeatures.Commands.UpdateCategory
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryValidator(IRepository<Category> repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100);

            RuleFor(x => x.IsActive)
                .NotNull();
        }
    }
}
