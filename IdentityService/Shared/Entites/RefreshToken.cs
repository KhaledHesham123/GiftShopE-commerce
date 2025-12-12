using Microsoft.EntityFrameworkCore;

namespace IdentityService.Shared.Entites
{
    [Owned]
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; set; }           
        public User User { get; set; } = default!;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}