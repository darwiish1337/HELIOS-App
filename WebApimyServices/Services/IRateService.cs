namespace WebApimyServices.Services
{
    public interface IRateService
    {
        Task AddRateAsync(string customerId, string factorId, decimal ratingValue);
        Task UpdateRateAsync(int rateId, decimal ratingValue);
        Task DeleteRateAsync(int rateId);
        Task<IEnumerable<Rate>> GetRatesForFactorAsync(string factorId);
    }
}
