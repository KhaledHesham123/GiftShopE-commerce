using Domain_Layer.Respones;
using IdentityService.Shared.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;   

namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public record ResetPasswordCommend(Guid Userid, string NewPassword) : IRequest<RequestRespones<bool>>;

    public class ResetPasswordCommendHandler : IRequestHandler<ResetPasswordCommend, RequestRespones<bool>>
    {
        private readonly IGenericRepository<Shared.Entites.User> genaricRepository;
        private readonly IGenericRepository<Shared.Entites.UserToken> tokenRepo;

        public ResetPasswordCommendHandler(IGenericRepository<Shared.Entites.User> genaricRepository, IGenericRepository<Shared.Entites.UserToken> tokenRepo)
        {
            this.genaricRepository = genaricRepository;
            this.tokenRepo = tokenRepo;
        }
        public async Task<RequestRespones<bool>> Handle(ResetPasswordCommend request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await genaricRepository.GetByCriteriaAsync(x=>x.Id==request.Userid);

                if (user == null)
                    return RequestRespones<bool>.Result(false,"User not found during update");


                user.PasswordHash = GeneratePasswordHash(request.NewPassword);

                genaricRepository.SaveInclude(user);

                var oldTokens = await tokenRepo.GetQueryableByCriteria(t => t.UserId == request.Userid).ToListAsync();
                if (oldTokens.Any())
                {
                    tokenRepo.DeleteRange(oldTokens); 
                }
                await genaricRepository.SaveChangesAsync();

                return RequestRespones<bool>.Success(true);
            }
            catch (Exception ex)
            {

                return RequestRespones<bool>.Fail("An error occurred while resetting the password: " + ex.Message,400);
            }
           
        }

        private  string GeneratePasswordHash(string realPassword)
        {
            return   BCrypt.Net.BCrypt.HashPassword(realPassword);
        }
    }

    
} 


