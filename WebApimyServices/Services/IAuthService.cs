namespace WebApimyServices.Services
{
    public interface IAuthService
    {
        Task<AuthModelDto> RegisterCustomerAsync(RegistertionCustomerDto registertionCustomerDto);
        Task<AuthModelDto> RegisterFactorAsync(RegistertionFactorDto registertionFactorDto);
        Task<AuthModelLoginDto> LoginAsync(LoginUserDto loginUserDto);
        Task<AuthModelDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
