namespace WebApimyServices.Hangfire
{
    public class ImageCleanupService : IImageCleanupService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IServiceProvider _provider;
        private readonly string _imagesPath;
        private readonly ILogger<ImageCleanupService> _logger;

        public ImageCleanupService(IWebHostEnvironment webHostEnvironment, IServiceProvider provider, ILogger<ImageCleanupService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSettings.ImagePathProfile); 
            _provider = provider;
            _logger = logger;
        }

        public void CleanupOrphanedImages()
        {
            try
            {
                // Check if the directory exists
                if (!Directory.Exists(_imagesPath))
                {
                    _logger.LogWarning($"Directory {_imagesPath} does not exist.");
                    return;
                }

                // Get list of all image files in _imagesPath
                string[] imageFiles = Directory.GetFiles(_imagesPath, "*.*", SearchOption.TopDirectoryOnly);

                foreach (string imagePath in imageFiles)
                {
                    string fileName = Path.GetFileName(imagePath);
                    string userId = Path.GetFileNameWithoutExtension(fileName);

                    // Check if user with userId exists
                    using (var scope = _provider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // Replace with your actual DbContext type
                        var user = dbContext.Users.SingleOrDefault(u => u.Id.ToString() == userId);

                        if (user == null)
                        {
                            // User does not exist, delete the image
                            File.Delete(imagePath);
                            _logger.LogInformation($"Deleted orphaned image: {imagePath}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred during image cleanup: {ex.Message}");
                throw; // Re-throw the exception to ensure it's handled appropriately
            }
        }
    }
}
