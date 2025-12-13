namespace IdentityService.Features.Authantication.Commands.ResetPassword
{
    public class ResetPasswordViewModle
    {
        public string Email { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
