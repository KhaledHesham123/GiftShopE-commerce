using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using IdentityService.Shared.Respones;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.Authantication.Queries.GetPermissionsByUserId
{
    public class GetPermissionsByUserIdQueryHandler(IGenericRepository<UserRole> _permissionRepository) : IRequestHandler<GetPermissionsByUserIdQuery, ResponseResult<IEnumerable<GetPermissionsByUserIdDTO>>>
    {
        public async Task<ResponseResult<IEnumerable<GetPermissionsByUserIdDTO>>> Handle(GetPermissionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userPermissions =await _permissionRepository.GetAll()
                                                            .Where(ur => ur.UserId == request.userId)
                                                            .SelectMany(ur => ur.Role.RolePermissions)
                                                            .Select(rp => new GetPermissionsByUserIdDTO
                                                            {
                                                                PermissionId = rp.Permission.Id,
                                                                PermissionName = rp.Permission.Name
                                                            })
                                                            .Distinct()
                                                            .ToListAsync();
          
            return userPermissions != null && userPermissions.Any()
                ? ResponseResult<IEnumerable<GetPermissionsByUserIdDTO>>.SuccessResponse(userPermissions, "Permissions retrieved successfully.")
                : ResponseResult<IEnumerable<GetPermissionsByUserIdDTO>>.FailResponse("No permissions found for the specified user.");
        }
    }
}
