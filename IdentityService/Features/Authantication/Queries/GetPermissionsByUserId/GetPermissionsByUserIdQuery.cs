using IdentityService.Shared.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Queries.GetPermissionsByUserId
{
    public record GetPermissionsByUserIdQuery(Guid userId) : IRequest<ResponseResult<IEnumerable<GetPermissionsByUserIdDTO>>>;
  
}
