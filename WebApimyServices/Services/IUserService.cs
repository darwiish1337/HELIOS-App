namespace WebApimyServices.Services
{
    public interface IUserService
    {
        Task<ApplicationUser?> Update(ProfileDto profileDto);
        IEnumerable<UserDto> GetUsersWithProblems();
        IEnumerable<ApplicationUser> Search(string query);
        Task<bool> ChangePassowrd(ChangePasswordDto changePasswordDto, string id);
    }
}
