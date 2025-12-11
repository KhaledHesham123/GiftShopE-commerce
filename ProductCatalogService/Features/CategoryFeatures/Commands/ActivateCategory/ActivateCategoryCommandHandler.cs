using MediatR;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.ActivateCategory
{
    public class ActivateCategoryCommandHandler : IRequestHandler<ActivateCategoryCommand, Result<ActivateCategoryDTO>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public ActivateCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<ActivateCategoryDTO>> Handle(ActivateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
                return Result<ActivateCategoryDTO>.FailResponse("Category not found.", new List<string> { "Category not found." }, 404);

            category.Status = request.IsActive;

            _categoryRepository.SaveInclude(category, nameof(category.Status));

            return Result<ActivateCategoryDTO>.SuccessResponse(new ActivateCategoryDTO { Id = category.Id, IsActive = category.Status }, "Category status updated successfully.", 200);
        }
    }
}
