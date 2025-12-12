using IdentityService.Shared.Enums;

namespace IdentityService.Shared.Entites
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PhoneNumber { get; set; } = string.Empty;
        public Gender Gender { get; set; } 
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public ICollection<RefreshToken> RefreshTokens = new HashSet<RefreshToken>();
        public IEnumerable<UserToken> UserTokens { get; set; }= new HashSet<UserToken>();

    }
}
