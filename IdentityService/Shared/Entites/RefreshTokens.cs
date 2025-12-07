namespace IdentityService.Shared.Entites
{
    public class RefreshTokens:BaseEntity
    {
        public Guid userid { get; set; } = default!;

        public User User { get; set; } = default!;


        public string Token { get; set; } = default!;
        public bool IsUsed { get; set; } = false; 


        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;    
        public DateTime? RevokedOn { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}