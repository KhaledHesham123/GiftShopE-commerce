using Domain_Layer.Respones;
using IdentityService.Features.User.DTOs;
using IdentityService.Shared.Entites;
using IdentityService.Shared.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.User.Quries.GetuserbyEmail
{
    public record GetUserByEmailQuery(string UserEmail):IRequest<RequestRespones<UserToReturnDto>>;

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, RequestRespones<UserToReturnDto>>
    {
        private readonly IGenericRepository<Shared.Entites.User> genaricRepository;

        public GetUserByEmailQueryHandler(IGenericRepository<Shared.Entites.User> genaricRepository)
        {
            this.genaricRepository = genaricRepository;
        }
        public async Task<RequestRespones<UserToReturnDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
           var user = await genaricRepository.GetQueryableByCriteria(u => u.Email == request.UserEmail)
                .Select(x => new UserToReturnDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    //Username = x.Username
                }).FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return RequestRespones<UserToReturnDto>.Fail("User not found", 404);
            }
           
            return RequestRespones<UserToReturnDto>.Success(user, 200, "User retrieved successfully");

        }
    }


}
