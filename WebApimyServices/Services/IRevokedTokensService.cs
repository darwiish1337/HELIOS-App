namespace WebApimyServices.Services
{
    public interface IRevokedTokensService
    {
        bool IsTokenRevoked(string tokenId);
        void RevokeToken(string tokenId);
    }
}
