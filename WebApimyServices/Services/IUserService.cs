namespace WebApimyServices.Services
{
    public interface IUserService
    {
        Task<UpdateResult> Update(ProfileDto profileDto);
        IEnumerable<UserDto> GetUserInCustomerRoleWithProblems();
        IEnumerable<FactorDto> GetUserInFactorRole();
        IEnumerable<ApplicationUser> Search(string query);
        Task<bool> ChangePassowrd(ChangePasswordDto changePasswordDto, string id);
        Task<UserJobInfoDto> AddTitleAndDescriptionAsync(string userId, UserJobInfoDto dto);
        Task<UserJobInfoDto> UpdateTitleAndDescriptionAsync(string userId, UserJobInfoDto dto);
        Task<int> DeleteTitleAndDescriptionAsync(string userId);
    }
}
