namespace WebApimyServices.Hangfire
{
    public class UserCleanupService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserCleanupService> _logger;
        private readonly string _imagesPath;

        public UserCleanupService(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            IEmailService emailService,
            ILogger<UserCleanupService> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
            _logger = logger;
            _imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSettings.ImagePathProfile);
        }

        public async Task CleanupInactiveUsers()
        {
            var thresholdDate = DateTime.UtcNow.AddDays(-30);

            var inactiveUsers = _context.Users
                .Where(u => u.LastActive < thresholdDate)
                .ToList();

            foreach (var user in inactiveUsers)
            {
                await DeleteUserAsync(user);
                await SendAccountRemovedMessage(user.Email);
            }
        }

        private async Task DeleteUserAsync(ApplicationUser user)
        {
            // Remove user's image
            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                var imagePath = Path.Combine(_imagesPath, user.ProfilePicture);

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    _logger.LogInformation($"Deleted image for user {user.Id} at path {imagePath}");
                }
            }

            // Remove associated problems if customer
            if (user.UserType == RoleConstants.Customer)
            {
                await _context.Entry(user).Collection(u => u.Problems).LoadAsync();
                var problems = user.Problems.ToList();
                _context.Problems.RemoveRange(problems);
                _logger.LogInformation($"Deleted {problems.Count} problems for user {user.Id}");
            }

            // Remove rates if factor
            if (user.UserType == RoleConstants.Factor)
            {
                await _context.Entry(user).Collection(u => u.ReceivedRates).LoadAsync();
                var rates = user.ReceivedRates.ToList();
                _context.Rates.RemoveRange(rates);
                _logger.LogInformation($"Deleted {rates.Count} rates for user {user.Id}");
            }

            // Remove the user
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted user {user.Id}");
        }

        private async Task SendAccountRemovedMessage(string email)
        {
            var logoUrl = "https://i.postimg.cc/v811BYfy/HELIOS-LOGO.png";

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
            <p>Your account has been removed from our system due to inactivity for 30 days.</p>
            <p>Please create a new account if you want to use our services.</p>
            </div>
            <div class='footer'>
                    &copy; 2024 HELIOS. All rights reserved.
            </div>
            </div>
            </body>
            </html>";

                await _emailService.SendEmailAsync(email, subject, body);
                _logger.LogInformation($"Email sent to {email}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {email}. Error: {ex.Message}");
            }
        }
    }
}
