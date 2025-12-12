using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using IdentityService.Shared.Respones;
using IdentityService.Shared.Services;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.Login
{
    public class LoginCommandHandler(IGenericRepository<Shared.Entites.User> _userRepository,IGenericRepository<RefreshToken> _refreshTokenRepository , IAuthService _authService) : IRequestHandler<LoginCommand, ResponseResult<AuthModel>>
    {
        public async Task<ResponseResult<AuthModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user =await _userRepository.GetByCriteriaAsync(u => u.Email == request.Email);
            if (user == null) 
                return ResponseResult<AuthModel>.FailResponse("Invalid email or password.", errors: ["incorrect email or password"], statusCode: 401);
            var passwrdCheck =await _authService.CheckPasswordAsync(user, request.Password);
            if (!passwrdCheck)
                return ResponseResult<AuthModel>.FailResponse("Invalid email or password.", errors: ["incorrect email or password"], statusCode: 401);
            var authModel = await _authService.GenerateTokensAsync(user);    
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = authModel.RefreshToken!,
                ExpiresOn = authModel.RefreshTokenExpiration!.Value
            };
            await _refreshTokenRepository.AddAsync(refreshToken);
            return ResponseResult<AuthModel>.SuccessResponse(authModel, "Login successful.");
        }
    }
}
