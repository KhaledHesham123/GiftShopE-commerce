using System.Data;

namespace IdentityService.Shared.Entites
{
    public class UserRole:BaseEntity
    {
        public Guid UserId { get; set; } = default!;
        public User User { get; set; } = default!;

        public Guid RoleId { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}