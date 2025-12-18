using MediatR;
using ProductCatalogService.Features.CategoryFeatures.Commands.ActivateCategory;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.ActivateProduct
{
    public record ActivateProductCommand(Guid Id, bool IsActive) : IRequest<Result<ActivateProductDto>>;

}
