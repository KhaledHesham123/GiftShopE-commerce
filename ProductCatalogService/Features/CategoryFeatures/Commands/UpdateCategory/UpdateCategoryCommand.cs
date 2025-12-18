using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, bool IsActive) : IRequest<Result<UpdateCategoryDTO>>;
}
