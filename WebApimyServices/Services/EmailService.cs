namespace WebApimyServices.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<EmailConfiguration> emailOptions, ILogger<EmailService> logger)
        {
            _emailConfig = emailOptions.Value;
            _logger = logger;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            try
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

                using var smtp = new SmtpClient();
                    smtp.Connect(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_emailConfig.Email, _emailConfig.Password);
                    await smtp.SendAsync(email);

                smtp.Disconnect(true);

                _logger.LogInformation($"Email sent to {mailTo}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {mailTo}. Error: {ex.Message}");
                throw;
            }
        }
    }
}