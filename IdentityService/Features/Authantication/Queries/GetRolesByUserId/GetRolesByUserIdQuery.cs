using IdentityService.Shared.Respones;
using MediatR;

namespace IdentityService.Features.Authantication.Queries.GetRolesByUserId
{
    public record GetRolesByUserIdQuery(Guid UserId) : IRequest<ResponseResult<IEnumerable<GetRolesByUserIdDTO>>>;
  
}
