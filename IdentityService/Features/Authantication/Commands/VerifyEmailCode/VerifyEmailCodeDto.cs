namespace IdentityService.Features.Authantication.Commands.VerifyEmailCode
{
    public class VerifyEmailCodeDto
    {
        public string email { get; set; }
        public string token { get; set; }
    }
}
