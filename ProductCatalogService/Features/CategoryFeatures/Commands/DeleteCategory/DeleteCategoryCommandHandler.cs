using MediatR;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Features.Shared;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.CategoryFeatures.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<DeleteCategoryDTO>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<DeleteCategoryDTO>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(request.Id, cancellationToken);

            if (!categoryExists)
                return Result<DeleteCategoryDTO>.FailResponse("Category not found.", new List<string> { "Category not found." }, 404);

            // ensure no products depend on this category (only not-deleted products)
            var hasProducts = await _categoryRepository.Get()
                .AnyAsync(c => c.Id == request.Id /*&& c.Products.Any()*/, cancellationToken);

            if (hasProducts)
                return Result<DeleteCategoryDTO>.FailResponse("Category cannot be deleted because products depend on it.",
                    new List<string> { "Category cannot be deleted because products depend on it." }, 400);

            await _categoryRepository.DeleteAsync(request.Id, cancellationToken);

            return Result<DeleteCategoryDTO>.SuccessResponse(new DeleteCategoryDTO { Id = request.Id }, "Category deleted successfully.", 200);
        }
    }
}
