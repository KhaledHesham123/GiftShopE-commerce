using System.Text.Json.Serialization;

namespace IdentityService.Features
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpiresOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
