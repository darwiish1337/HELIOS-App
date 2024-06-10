namespace WebApimyServices.Services
{
    public interface IAuthService
    {
        Task<AuthModelDto> RegisterCustomerAsync(RegistertionCustomerDto registertionCustomerDto);
        Task<AuthModelDto> RegisterFactorAsync(RegistertionFactorDto registertionFactorDto);
        Task<AuthModelDto> LoginAsync(LoginUserDto loginUserDto);
        Task<AuthModelDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
