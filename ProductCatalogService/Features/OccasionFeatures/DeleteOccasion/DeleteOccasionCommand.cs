using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.OccasionFeatures.DeleteOccasion
{
    public record DeleteOccasionCommand(Guid occasionId) : IRequest<Result<DeleteOccasionDTO>>
    {
    }

}
