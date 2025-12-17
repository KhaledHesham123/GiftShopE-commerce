using MediatR;
using UserProfileService.Features.Shared;

namespace UserProfileService.Features.CategoryFeatures.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, bool IsActive) : IRequest<Result<CreateCategoryDTO>>;
}
