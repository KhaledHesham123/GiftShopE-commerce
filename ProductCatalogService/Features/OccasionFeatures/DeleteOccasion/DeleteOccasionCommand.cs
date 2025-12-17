using MediatR;
using UserProfileService.Features.Shared;

namespace UserProfileService.Features.OccasionFeatures.DeleteOccasion
{
    public record DeleteOccasionCommand(Guid occasionId) : IRequest<Result<DeleteOccasionDTO>>
    {
    }

}
