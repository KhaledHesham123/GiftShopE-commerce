using MediatR;
using UserProfileService.Features.Shared;

namespace UserProfileService.Features.CategoryFeatures.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, bool IsActive) : IRequest<Result<UpdateCategoryDTO>>;
}
