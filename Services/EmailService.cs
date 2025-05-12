using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace Sakan_project.Services
{

    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            emailMessage.To.Add(MailboxAddress.Parse(toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(
                    _config["EmailSettings:SmtpServer"],
                    int.Parse(_config["EmailSettings:Port"]),
                    SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
            }
        }
    }

}

