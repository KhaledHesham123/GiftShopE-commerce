using System.Data;

namespace IdentityService.Shared.Entites
{
    public class UserRole:BaseEntity
    {
        public Guid Userid { get; set; } = default!;
        public User user { get; set; } = default!;

        public Guid Roleid { get; set; } = default!;

        public Role role { get; set; } = default!;
    }
}