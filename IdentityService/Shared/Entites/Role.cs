namespace IdentityService.Shared.Entites
{
    public class Role:BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }
}