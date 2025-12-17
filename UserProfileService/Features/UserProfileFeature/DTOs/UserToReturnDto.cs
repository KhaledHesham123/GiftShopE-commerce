using UserProfileService.Features.UserAddressFeature.DTOs;

namespace UserProfileService.Features.UserProfileFeature.DTOs
{
    public class UserToReturnDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ProfileImageUrl { get; set; }

        public ICollection<UserAddressDTo> Addresses { get; set; }
    }
}
