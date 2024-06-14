namespace WebApimyServices.Services
{
    public interface IUserService
    {
        Task<UpdateResult> Update(ProfileDto profileDto);
        IEnumerable<UserDto> GetUserInCustomerRoleWithProblems();
        IEnumerable<UserDto> GetUserInFactorRole();
        IEnumerable<ApplicationUser> Search(string query);
        Task<bool> ChangePassowrd(ChangePasswordDto changePasswordDto, string id);
    }
}
