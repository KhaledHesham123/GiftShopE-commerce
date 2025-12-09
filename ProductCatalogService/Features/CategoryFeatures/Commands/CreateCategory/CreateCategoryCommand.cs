using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, bool IsActive) : IRequest<Result<CreateCategoryDTO>>;
}
