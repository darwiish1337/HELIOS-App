namespace WebApimyServices.Services
{
    public class RevokedTokensService : IRevokedTokensService
    {
        private readonly ApplicationDbContext _context;

        public RevokedTokensService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsTokenRevoked(string tokenId)
        {
            return _context.RevokedTokens.Any(rt => rt.TokenId == tokenId);
        }

        public void RevokeToken(string tokenId)
        {
            if (!_context.RevokedTokens.Any(rt => rt.TokenId == tokenId))
            {
                _context.RevokedTokens.Add(new RevokedToken { TokenId = tokenId, RevokedAt = DateTime.UtcNow });
                _context.SaveChanges();
            }
        }
    }
}
