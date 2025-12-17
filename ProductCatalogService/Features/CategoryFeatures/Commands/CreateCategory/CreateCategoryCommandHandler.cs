using MediatR;
using UserProfileService.Features.Shared;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.CategoryFeatures.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryDTO>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public CreateCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CreateCategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var exists = await _categoryRepository.ExistsAsync(c => c.Name == request.Name, cancellationToken);

            if (exists)
            {
                return Result<CreateCategoryDTO>.FailResponse("Category with the same name already exists.",
                    ["Category with the same name already exists."], 409);
            }

            var category = new Category
            {
                Name = request.Name,
                Status = request.IsActive
            };

            await _categoryRepository.AddAsync(category, cancellationToken);

            return Result<CreateCategoryDTO>.SuccessResponse(new CreateCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            }, "Category created successfully.", 201);
        }
    }
}
