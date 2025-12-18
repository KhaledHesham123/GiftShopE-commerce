using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.ActivateCategory
{
    public record ActivateCategoryCommand(Guid Id, bool IsActive) : IRequest<Result<ActivateCategoryDTO>>;
}
