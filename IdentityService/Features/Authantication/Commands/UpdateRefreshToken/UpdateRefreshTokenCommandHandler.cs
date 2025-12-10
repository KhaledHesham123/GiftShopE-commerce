using IdentityService.Shared.Repository;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenCommandHandler(IGenericRepository<Shared.Entites.User> _userRepository) : IRequestHandler<UpdateRefreshTokenCommand, bool>
    {
        public async Task<bool> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _userRepository.Update(request.User);
            await _userRepository.SaveChangesAsync();
            return true;

        }
    }
}
