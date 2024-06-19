namespace WebApimyServices.Services
{
    public class RevokedTokensService : IRevokedTokensService
    {
        private readonly HashSet<string> _revokedTokens = new HashSet<string>();

        public bool IsTokenRevoked(string token)
        {
            return _revokedTokens.Contains(token);
        }

        public void RevokeToken(string token)
        {
            _revokedTokens.Add(token);
        }
    }
}
