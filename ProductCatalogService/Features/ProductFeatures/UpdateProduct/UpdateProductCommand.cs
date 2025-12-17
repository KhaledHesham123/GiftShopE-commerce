using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    bool Status,
    Guid CategoryId,
    List<Guid> OccasionIds,
    List<string> Tags,
    List<IFormFile>? NewImages,
    List<Guid>? RemovedImageIds) : IRequest<Result<string>>;
}
