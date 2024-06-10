namespace WebApimyServices.Services
{
    public interface IJwtUtils
    {
        public void ExpireToken(string token);
    }
}
