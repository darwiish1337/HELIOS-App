namespace WebApimyServices.Hangfire
{
    public interface IImageCleanupService
    {
        void CleanupOrphanedImages();
    }
}
