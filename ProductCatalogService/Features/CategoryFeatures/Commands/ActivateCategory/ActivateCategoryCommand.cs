using MediatR;
using UserProfileService.Features.Shared;

namespace UserProfileService.Features.CategoryFeatures.Commands.ActivateCategory
{
    public record ActivateCategoryCommand(Guid Id, bool IsActive) : IRequest<Result<ActivateCategoryDTO>>;
}
