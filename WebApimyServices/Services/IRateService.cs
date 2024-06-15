namespace WebApimyServices.Services
{
    public interface IRateService
    {
        Task<Rate[]> GetAllForUserAsync(string userId);
        Task<Rate> GetByIdAsync(int id);
        Task<Rate> CreateAsync(Rate model);
        Task<Rate> UpdateAsync(Rate model);
        Task<Rate> DeleteAsync(int id);
    }
}
