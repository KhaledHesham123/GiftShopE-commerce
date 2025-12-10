namespace IdentityService.Shared.Services.EmailVerificationServices
{
    public interface IEmailVerificationService
    {

        Task SendVerificationEmailAsync(string toEmail, string code);

    }
}
