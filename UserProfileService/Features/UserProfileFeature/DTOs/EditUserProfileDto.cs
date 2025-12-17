namespace UserProfileService.Features.UserProfileFeature.DTOs
{
    public class EditUserProfileDto
    {
        public Guid userid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
