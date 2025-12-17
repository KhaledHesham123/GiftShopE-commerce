using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.ProductFeatures.CreateProduct
{
    public record CreateProductCommand(
    string Name, 
    string Description, 
    decimal Price , 
    bool Status,
    Guid CategoryId,
    List<Guid> OccasionIds,
    List<string> Tags,
    List<IFormFile> Images) : IRequest<Result<Guid>>;


}
