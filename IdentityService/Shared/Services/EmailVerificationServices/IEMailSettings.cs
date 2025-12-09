using IdentityService.Shared.Entites;

namespace IdentityService.Shared.Services.EmailVerificationServices
{
    public interface IEMailSettings
    {
        void sendEmail(Email email);
    }
}
