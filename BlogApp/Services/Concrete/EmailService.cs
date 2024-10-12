using BlogApp.Services.Abstract;
using MailKit.Net.Smtp;
using MimeKit;

namespace BlogApp.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = _configuration.GetValue<string>("Mail:mail");
            var key = _configuration.GetValue<string>("Mail:key");


            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", mail);

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(mail, key);
            client.Send(mimeMessage);
            client.Disconnect(true);
            return Task.CompletedTask;
        }
    }
}
