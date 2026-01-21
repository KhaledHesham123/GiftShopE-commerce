namespace IdentityService.Features.User.DTOs
{
    public class UserToReturnDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;

        //
    }
}
