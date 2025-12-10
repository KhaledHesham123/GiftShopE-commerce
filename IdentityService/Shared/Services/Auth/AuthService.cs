using IdentityService.Features;
using IdentityService.Features.Authantication;
using IdentityService.Features.Authantication.Commands.UpdateRefreshToken;
using IdentityService.Features.Authantication.Queries.GetPermissionsByUserId;
using IdentityService.Features.Authantication.Queries.GetRolesByUserId;
using IdentityService.Features.Authantication.Queries.GetUserByRefreshToken;


//using IdentityService.Features.Authantication.Commands.AddRefreshToken;
//using IdentityService.Features.Authantication.Queries.GetPermissionsByUserId;
//using IdentityService.Features.Authantication.Queries.GetRefreshTokenByUserId;
//using IdentityService.Features.Authantication.Queries.GetRolesByUserId;
//using IdentityService.Features.Authantication.Queries.GetUserByRefreshToken;
using IdentityService.Shared.Entites;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;

        public AuthService(IConfiguration config, IMediator mediator)
        {
            _config = config;
            _mediator = mediator;
        }

        public async Task<string> GeneratePasswordHashAsync(string realPassword)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(realPassword));
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.Verify(password, user.PasswordHash));
        }

        private async Task ChangePasswordAsync(int userId, string password)
        {
            throw new NotImplementedException();
        }
        public async Task<AuthModel> GenerateTokensAsync(User user)
        {
            var authModel = new AuthModel
            {
                IsAuthenticated = true
            };

            // ACCESS TOKEN =======================
            var jwt = await CreateJwtTokenAsync(user);
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
            authModel.TokenExpiresOn = jwt.ValidTo;

            // REFRESH TOKEN ======================
            //var refreshToken = (await _mediator.Send(new GetRefreshTokenByUserIdQuery(user.Id))).Data;
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _mediator.Send(new UpdateRefreshTokenCommand(user));
            }
            //if (refreshToken is null)
            //{
            //    refreshToken = GenerateRefreshToken();
            //    refreshToken.UserId = user.Id;
            //    await _mediator.Send(new AddRefreshTokenCommand(refreshToken));
            //}
            //authModel.RefreshToken = refreshToken.Token;
            //authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

            return authModel;
        }
        public async Task<AuthModel> RefreshTokenAsync(string refreshToken)
        {
            var user = await _mediator.Send(new GetUserByRefreshTokenQuery(refreshToken));
            if (!user.Success)
                return new AuthModel { IsAuthenticated = false };
            var token = user.Data.RefreshTokens.Single(t => t.Token == refreshToken);
            //var token = (await _mediator.Send(new GetRefreshTokenByUserIdQuery(user.Data.Id))).Data;
            if (token is null || !token.IsActive)
                return new AuthModel { IsAuthenticated = false };

            token.RevokedOn = DateTime.UtcNow;
            //var newTokens = await GenerateTokensAsync(user.Data);      => old 


            var newRefreshToken = GenerateRefreshToken();
            user.Data.RefreshTokens.Add(newRefreshToken);
            await _mediator.Send(new UpdateRefreshTokenCommand(user.Data));

            var newjwtToken = await CreateJwtTokenAsync(user.Data);
            AuthModel authModel = new AuthModel
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(newjwtToken),
                TokenExpiresOn = newjwtToken.ValidTo,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresOn
            };
            return authModel;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var random = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(random),
                CreatedAt = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(10)
            };
        }
        private async Task<JwtSecurityToken> CreateJwtTokenAsync(User user)
        {
            // CREATE CLAIMS ========================
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Aud, _config["JWT:Audience"]),
                new Claim(JwtRegisteredClaimNames.Iss, _config["JWT:Author"])
            };

            // Add Roles
            var userRoles = (await _mediator.Send(new GetRolesByUserIdQuery(user.Id))).Data.Select(s => s.RoleName);
            foreach (var role in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // Add Permissions
            var userPermissions = (await _mediator.Send(new GetPermissionsByUserIdQuery(user.Id))).Data.Select(s => s.PermissionName);
            foreach (var perm in userPermissions)
                claims.Add(new Claim("permission", perm));

            // CREATE ACCESS TOKEN ========================
            var keyInByte = Encoding.ASCII.GetBytes(_config["JWT:Key"]);
            SymmetricSecurityKey key = new SymmetricSecurityKey(keyInByte);
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["jwt:ExpirationInMinutes"])));

            return jwt;
        }
    }
}
