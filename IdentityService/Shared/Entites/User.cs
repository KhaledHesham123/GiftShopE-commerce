namespace IdentityService.Shared.Entites
{
    public class User:BaseEntity
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public bool EmailConfirmed { get; set; } = false;

        public IEnumerable<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public IEnumerable<RefreshTokens> refreshTokens = new HashSet<RefreshTokens>();

        public IEnumerable<UserToken> UserTokens { get; set; }= new HashSet<UserToken>();

    }
}
