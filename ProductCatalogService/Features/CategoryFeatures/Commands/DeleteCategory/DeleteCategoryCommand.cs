using MediatR;
using UserProfileService.Features.Shared;

namespace UserProfileService.Features.CategoryFeatures.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Result<DeleteCategoryDTO>>;
}
