using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.DeleteProduct
{
    public record DeleteProductCommand(Guid productId) : IRequest<Result<string>>;
}
