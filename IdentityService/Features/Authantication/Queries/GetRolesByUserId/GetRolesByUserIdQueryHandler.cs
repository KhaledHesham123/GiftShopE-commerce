using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using IdentityService.Shared.Respones;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.Authantication.Queries.GetRolesByUserId
{
    public class GetRolesByUserIdQueryHandler(IGenericRepository<Role> _roleRepository) : IRequestHandler<GetRolesByUserIdQuery, ResponseResult<IEnumerable<GetRolesByUserIdDTO>>>
    {
        public async Task<ResponseResult<IEnumerable<GetRolesByUserIdDTO>>> Handle(GetRolesByUserIdQuery request, CancellationToken cancellationToken)
        {

            var userRoles = await _roleRepository.GetAll()
                  .Where(w => w.UserRoles.Any(u => u.UserId == request.UserId))
                  .Select(role => new GetRolesByUserIdDTO
                  {
                      RoleId = role.Id,
                      RoleName = role.Name
                  }).ToListAsync();

            return userRoles is not null && userRoles.Any()
               ? ResponseResult<IEnumerable<GetRolesByUserIdDTO>>.SuccessResponse(userRoles, "roles retrieved successfully")
               : ResponseResult<IEnumerable<GetRolesByUserIdDTO>>.FailResponse("No roles found for the specified user.");
            throw new NotImplementedException();
        }
    }
}
