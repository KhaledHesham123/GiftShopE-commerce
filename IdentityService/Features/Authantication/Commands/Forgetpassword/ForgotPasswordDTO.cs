namespace IdentityService.Features.Authantication.Commands.Forgetpassword
{
    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
