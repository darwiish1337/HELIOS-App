namespace WebApimyServices.Middelwares
{
    public class UpdateLastActiveMiddleware : IMiddleware
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UpdateLastActiveMiddleware> _logger;

        public UpdateLastActiveMiddleware(ApplicationDbContext dbContext, ILogger<UpdateLastActiveMiddleware> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("UpdateLastActiveMiddleware invoked.");

            try
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var userEmailClaim = context.User.FindFirst(ClaimTypes.Email)?.Value;

                    if (string.IsNullOrEmpty(userEmailClaim))
                    {
                        _logger.LogWarning("User email claim is null or empty.");
                        await next(context);
                        return;
                    }

                    // Log the retrieved claim value
                    _logger.LogInformation($"User email claim value: '{userEmailClaim}'");

                    var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmailClaim);

                    if (user != null)
                    {
                        user.LastActive = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync(); // Save changes to the database
                        _logger.LogInformation($"User with email '{userEmailClaim}' last active updated.");
                    }
                    else
                    {
                        _logger.LogWarning($"User with email '{userEmailClaim}' not found in the database.");
                    }
                }
                else
                {
                    _logger.LogInformation("User is not authenticated.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in UpdateLastActiveMiddleware.");
            }

            await next(context);
        }
    }
}
