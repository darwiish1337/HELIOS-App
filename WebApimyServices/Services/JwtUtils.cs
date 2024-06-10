namespace WebApimyServices.Services
{
    public class JwtUtils : IJwtUtils
    {
        public void ExpireToken(string token)
        {
            // Update the token's expiration time to now
            var tokenExpiration = DateTime.UtcNow;
        }
    }
}
