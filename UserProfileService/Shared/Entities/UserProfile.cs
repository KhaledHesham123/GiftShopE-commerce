using System.ComponentModel.DataAnnotations;

namespace UserProfileService.Shared.Entities
{
    public class UserProfile : BaseEntity
    {
        public string IdentityUserId { get; set; } // FK to Identity Service

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ProfileImageUrl { get; set; }

        public ICollection<UserAddress> Addresses { get; set; }
    }
}
