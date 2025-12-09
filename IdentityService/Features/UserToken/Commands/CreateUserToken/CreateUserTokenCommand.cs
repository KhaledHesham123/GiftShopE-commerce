using Domain_Layer.Respones;
using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using MediatR;

namespace IdentityService.Features.UserToken.Commands.CreateUserToken
{
    public record CreateUserTokenCommand(Guid UserId,string token, int expiredInMin = 2):IRequest<RequestRespones<string>>;

    public class CreateUserTokenCommandHandler : IRequestHandler<CreateUserTokenCommand, RequestRespones<string>>
    {
        private readonly IGenaricRepository<Shared.Entites.UserToken> _userTokenRepository;

        public CreateUserTokenCommandHandler(IGenaricRepository<Shared.Entites.UserToken> genaricRepository)
        {
            this._userTokenRepository = genaricRepository;
        }

        public async Task<RequestRespones<string>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var userToken = await _userTokenRepository.FirstOrDefaultAsync(r => r.UserId == request.UserId);

            if (userToken == null)
            {
                userToken = new Shared.Entites.UserToken() 
                {
                    UserId = request.UserId,
                    Token = request.token,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredDate = DateTime.UtcNow.AddMinutes(request.expiredInMin)
                };

                await _userTokenRepository.AddAsync(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }
            else
            {
                userToken.Token = request.token;
                userToken.CreatedAt = DateTime.UtcNow;
                userToken.ExpiredDate = DateTime.UtcNow.AddMinutes(request.expiredInMin);
            }

            return RequestRespones<string>.Success(userToken.Token);

        }
    }

}

