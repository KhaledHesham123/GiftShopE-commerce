namespace IdentityService.Shared.Entites
{
    public class UserPermission : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }

    }
}