namespace WebApimyServices.Services
{
    public class RateService : IRateService
    {
        private readonly ApplicationDbContext _context;

        public RateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRateAsync(string customerId, string factorId, decimal ratingValue)
        {
            // Ensure the rating value is in the scale of 1 to 5
            if (ratingValue < 1 || ratingValue > 5)
            {
                throw new InvalidOperationException("Rating value must be between 1 and 5.");
            }

            var rate = new Rate
            {
                CustomerId = customerId,
                FactorId = factorId,
                RatingValue = ratingValue,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();

            // After adding the rate, recalculate the average rating for the factor
            await UpdateAverageRatingAsync(factorId);
        }

        public async Task UpdateRateAsync(int rateId, decimal ratingValue)
        {
            var rate = await _context.Rates.FindAsync(rateId);

            if (rate == null)
            {
                throw new InvalidOperationException("Invalid rate ID.");
            }

            // Store the old rating value for average calculation
            decimal oldRatingValue = rate.RatingValue;

            // Update the rate with the new rating value
            rate.RatingValue = ratingValue;

            await _context.SaveChangesAsync();

            // After updating the rate, recalculate the average rating for the factor
            await UpdateAverageRatingAsync(rate.FactorId, oldRatingValue, ratingValue);
        }

        public async Task DeleteRateAsync(int rateId)
        {
            var rate = await _context.Rates.FindAsync(rateId);

            if (rate == null)
            {
                throw new InvalidOperationException("Invalid rate ID.");
            }

            // Store the old rating value for average calculation
            decimal oldRatingValue = rate.RatingValue;

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();

            // After deleting the rate, recalculate the average rating for the factor
            await UpdateAverageRatingAsync(rate.FactorId, oldRatingValue, 0);
        }

        public async Task<IEnumerable<Rate>> GetRatesForFactorAsync(string factorId)
        {
            return await _context.Rates
                .Where(r => r.FactorId == factorId)
                .ToListAsync();
        }

        #region Private Methods
        // Update Average Rate
        private async Task UpdateAverageRatingAsync(string factorId)
        {
            var rates = await _context.Rates
                .Where(r => r.FactorId == factorId)
                .ToListAsync();

            if (!rates.Any())
            {
                return;
            }

            // Calculate the average rating
            decimal averageRating = rates.Average(r => r.RatingValue);

            // Update the average rating for the factor
            var factor = await _context.Users.FindAsync(factorId);
            if (factor != null)
            {
                factor.AverageRating = averageRating;
                await _context.SaveChangesAsync();
            }
        }

        // Update With Updte Average Rate
        private async Task UpdateAverageRatingAsync(string factorId, decimal oldRatingValue, decimal newRatingValue)
        {
            var rates = await _context.Rates
                .Where(r => r.FactorId == factorId)
                .ToListAsync();

            if (!rates.Any())
            {
                return;
            }

            // Calculate the new sum of all ratings for the factor
            decimal sumOfRatings = rates.Sum(r => r.RatingValue) - oldRatingValue + newRatingValue;

            // Calculate the average rating
            decimal averageRating = sumOfRatings / rates.Count;

            // Update the average rating for the factor
            var factor = await _context.Users.FindAsync(factorId);
            if (factor != null)
            {
                factor.AverageRating = averageRating;
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}