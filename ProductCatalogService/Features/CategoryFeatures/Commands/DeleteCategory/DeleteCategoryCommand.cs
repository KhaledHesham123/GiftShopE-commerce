using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CategoryFeatures.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Result<DeleteCategoryDTO>>;
}
