using Domain_Layer.Respones;
using IdentityService.Shared.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.UserToken.Commands.RemoveUserToken
{
    public record RemoveUserTokenCommand(Guid userid):IRequest<RequestRespones<bool>>;


    public class RemoveUserTokenCommandHandler:IRequestHandler<RemoveUserTokenCommand,RequestRespones<bool>>
    {
        private readonly IGenericRepository<Shared.Entites.UserToken> genericRepository;

        public RemoveUserTokenCommandHandler(IGenericRepository<Shared.Entites.UserToken> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public async Task<RequestRespones<bool>> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
        {
            var oldTokens = await genericRepository.GetQueryableByCriteria(t => t.UserId == request.userid).ToListAsync();
            if (oldTokens.Any())
            {
                genericRepository.DeleteRange(oldTokens);
            }

            await genericRepository.SaveChangesAsync();

            return RequestRespones<bool>.Success(true);
        }
    }


}
