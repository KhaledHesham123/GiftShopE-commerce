using MediatR;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Respones;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.UserProfileFeature.Commands.EditUserProfile
{
    public record EditUserProfileCommand(Guid userid,string FirstName, string LastName, string Gender, string ProfileImageUrl) :IRequest<RequestRespones<bool>>;

    public class UserProfilToEditDtoHandler : IRequestHandler<EditUserProfileCommand, RequestRespones<bool>>
    {
        private readonly IRepository<UserProfile> repository;

        public UserProfilToEditDtoHandler(IRepository<UserProfile> repository)
        {
            this.repository = repository;
        }
        public async Task<RequestRespones<bool>> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await repository.GetQueryableByCriteria(x => x.Id == request.userid).FirstOrDefaultAsync();
                if (user == null)
                {
                    return RequestRespones<bool>.Fail("There is no user with this id");
                }
                var newuser = new UserProfile
                {
                    FirstName = request.FirstName ?? user.FirstName,
                    LastName = request.LastName ?? user.LastName,
                    Gender = request.Gender ?? user.Gender,
                    ProfileImageUrl = request.ProfileImageUrl ?? user.ProfileImageUrl
                };

                repository.SaveInclude(newuser,
                    nameof(user.FirstName),
                    nameof(user.LastName),
                    nameof(user.Gender),
                    nameof(user.ProfileImageUrl));

                await repository.SaveChangesAsync();

                return RequestRespones<bool>.Success(true);
            }
            catch (Exception ex)
            {

                return RequestRespones<bool>.Fail($"An unexpected error occurred: {ex.Message}");
            }
           

        }
    }



}
