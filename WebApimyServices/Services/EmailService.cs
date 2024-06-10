namespace WebApimyServices.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(IOptions<EmailConfiguration> emailOptions)
        {
            _emailConfig = emailOptions.Value;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailConfig.Email),
                Subject = subject
            };

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_emailConfig.DisplayName,_emailConfig.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfig.Host,_emailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfig.Email, _emailConfig.Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}