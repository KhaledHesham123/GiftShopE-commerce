using IdentityService.Shared.Configurations;
using IdentityService.Shared.Entites;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace IdentityService.Shared.Services.EmailVerificationServices
{
    public class EMailSettings : IEMailSettings
    {
        private readonly MailSettings options;

        public EMailSettings(IOptions<MailSettings> options)
        {
            this.options = options.Value;
        }
        public void sendEmail(Email email)
        {
            var mail = new MimeMessage
            {
                Sender= MailboxAddress.Parse(options.email),
                Subject= email.subject,
            };
            mail.To.Add(MailboxAddress.Parse(email.to));
            mail.From.Add( new MailboxAddress(options.Displyname,options.email));

            var builder = new BodyBuilder();

            builder.TextBody = email.body;

            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(options.Host,options.Port,SecureSocketOptions.StartTls);

            smtp.Authenticate(options.email, options.Password);
            smtp.Send(mail);

            smtp.Disconnect(true); 
        }
    }
}
