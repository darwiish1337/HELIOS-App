public class HangfireService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;
    private readonly ILogger<HangfireService> _logger;

    public HangfireService(ApplicationDbContext dbContext, IEmailService emailService, ILogger<HangfireService> logger)
    {
        _context = dbContext;
        _emailService = emailService;
        _logger = logger;
    }

    public void CheckAndRemoveUnconfirmedUsers()
    {
        var cutoff = DateTime.UtcNow.AddHours(1);

        var usersToRemove = _context.Users
            .Where(u => !u.EmailConfirmed && u.CreatedDate < cutoff)
            .ToList();

        foreach (var user in usersToRemove)
        {
            _context.Users.Remove(user);
            _logger.LogInformation($"Removed user with ID {user.Id} due to unconfirmed email and account age.");

            // Send an email to the user
            try
            {
                SendAccountRemovedMessage(user.Email).Wait(); // Await the async method
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {user.Email}. Error: {ex.Message}");
            }
        }

        _context.SaveChanges();
    }

    private async Task SendAccountRemovedMessage(string email)
    {
        var logoUrl = "https://i.postimg.cc/xdDcGhfS/HEL-removebg-preview.png";

        try
        {
            var subject = "Account Removed";

            var body = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                }}
                .header {{
                    text-align: center;
                    padding-bottom: 20px;
                    border-bottom: 1px solid #eaeaea;
                }}
                .content {{
                    padding: 20px 0;
                    text-align: center;
                }}
                .footer {{
                    text-align: center;
                    padding-top: 20px;
                    border-top: 1px solid #eaeaea;
                    font-size: 12px;
                    color: #999999;
                }}
            </style>
            </head>
            <body>
            <div class='container'>
            <div class='header'>
            <img src='{logoUrl}' alt='Company Logo' style='width: 30%; height: auto;'>
            </div>
            <div class='content'>
            <h1>Account Removed</h1>
            <p>Dear <b>{email}</b>,</p>
            <p>Your account has been removed from our system because you didn't confirm it within 1 hour of creation.</p>
            <p>Please create a new account if you want to use our services.</p>
            </div>
            <div class='footer'>
                    &copy; 2024 HELIOS. All rights reserved.
            </div>
            </div>
            </body>
            </html>";

            await _emailService.SendEmailAsync(email, subject, body); // Note the additional parameter to indicate HTML content
            _logger.LogInformation($"Email sent to {email}.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email to {email}. Error: {ex.Message}");
        }
    }
}