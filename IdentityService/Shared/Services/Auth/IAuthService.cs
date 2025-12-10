using IdentityService.Features;
using IdentityService.Shared.Entites;

namespace IdentityService.Shared.Services
{
    public interface IAuthService
    {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<string> GeneratePasswordHashAsync(string realPassword);
        Task<AuthModel> GenerateTokensAsync(User user);
        Task<AuthModel> RefreshTokenAsync(string refreshToken);
    }
}