using MediatR;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<UpdateCategoryDTO>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<UpdateCategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
                return Result<UpdateCategoryDTO>.FailResponse("Category not found.", new List<string> { "Category not found." }, 404);

            // check duplicate name excluding current category
            var exists = await _categoryRepository.ExistsAsync(c => c.Name == request.Name && c.Id != request.Id, cancellationToken);

            if (exists)
                return Result<UpdateCategoryDTO>.FailResponse("Category with the same name already exists.", new List<string> { "Category with the same name already exists." }, 409);

            category.Name = request.Name;
            category.Status = request.IsActive;

            _categoryRepository.SaveInclude(
                   category,
                   nameof(category.Name),
                   nameof(category.Status));

            return Result<UpdateCategoryDTO>.SuccessResponse(new UpdateCategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            }, "Category updated successfully.", 200);
        }
    }
}
