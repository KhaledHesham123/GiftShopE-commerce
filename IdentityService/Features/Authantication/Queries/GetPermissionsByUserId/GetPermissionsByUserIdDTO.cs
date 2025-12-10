namespace IdentityService.Features.Authantication.Queries.GetPermissionsByUserId
{
    public class GetPermissionsByUserIdDTO
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}
