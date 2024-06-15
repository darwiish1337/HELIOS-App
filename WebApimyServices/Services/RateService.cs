namespace WebApimyServices.Services
{
    public class RateService : IRateService
    {
        private readonly ApplicationDbContext _context;

        public RateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Rate[]> GetAllForUserAsync(string userId)
        {
            return _context.Rates
               .Where(r => r.Users.Any(u => u.Id == userId))
               .ToArray();
        }

        public async Task<Rate> GetByIdAsync(int id)
        {
            return await _context.Rates.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rate> CreateAsync(Rate model)
        {
            var id = _context.Rates.Count() > 0 ? _context.Rates.Max(r => r.Id) + 1 : 1;
            model.Id = id;
            var rateEntity = await _context.Rates.AddAsync(model);
            await _context.SaveChangesAsync();
            return rateEntity.Entity;
        }

        public async Task<Rate> UpdateAsync(Rate model)
        {
            var rateEntity = await _context.Rates.FindAsync(model.Id);
            if (rateEntity != null)
            {
                _context.Entry(rateEntity).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
            }
            return rateEntity;
        }

        public async Task<Rate> DeleteAsync(int id)
        {
            var rateEntity = await _context.Rates.FindAsync(id);
            if (rateEntity != null)
            {
                _context.Rates.Remove(rateEntity);
                await _context.SaveChangesAsync();
            }
            return rateEntity;
        }
    }
}
