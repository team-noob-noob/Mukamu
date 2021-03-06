using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Services;

namespace Sinuka.Infrastructure.Services
{
    public class GmailEmailService : IEmailService
    {
        public async Task SendEmail(string to, string message, string subject)
        {
            if (
                !string.IsNullOrEmpty(Sinuka.Infrastructure.Configurations.EmailConfig.Username)
                || !string.IsNullOrEmpty(Sinuka.Infrastructure.Configurations.EmailConfig.Password)
            )
            {
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress("DONOTREPLY@gmail.com"),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = message
                };

                mailMessage.To.Add(new MailAddress(to));

                SmtpClient smtp = new SmtpClient()
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                       Sinuka.Infrastructure.Configurations.EmailConfig.Username,
                       Sinuka.Infrastructure.Configurations.EmailConfig.Password
                    ),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                await Task.Run(() => smtp.Send(mailMessage));
            }
        }
    }
}
