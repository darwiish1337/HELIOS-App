namespace WebApimyServices.Services
{
    public interface IRevokedTokensService
    {
        void RevokeToken(string token);
        bool IsTokenRevoked(string token);
    }
}
