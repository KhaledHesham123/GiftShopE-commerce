namespace IdentityService.Shared.Entites
{
    public class Role:BaseEntity
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public IEnumerable<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}