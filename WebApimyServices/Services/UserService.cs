namespace WebApimyServices.Services
{
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathProfile}";
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> ChangePassowrd(ChangePasswordDto changePasswordDto, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                throw new NotFoundException("User not found");

            if (!(await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword)))
            {
                throw new InvalidOperationException("Current password is incorrect");
            }

            var result = await _userManager
              .ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to change password");
            }

            return true;
        }

        public IEnumerable<UserDto> GetUserInCustomerRoleWithProblems()
        {
            var users = _userManager.GetUsersInRoleAsync(RoleConstants.Customer).Result
                          .Select(u => new UserDto
                          {
                              Id = u.Id,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              UserType = u.UserType,
                              PhoneNumber = u.PhoneNumber,
                              DisplayName = u.DisplayName,
                              CityId = u.CityId,
                              CreatedDate = u.CreatedDate,
                              Job = u.Job,
                              ProfilePicture = u.ProfilePicture,
                              Email = u.Email,
                              Problems = u.Problems.Select(p => new ProblemDto
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  ProblemImg = p.ProblemImg,
                                  Description = p.Description,
                                  Status = p.Status,
                                  CreatedDate = p.CreatedDate,
                                  CategoryId = p.CategoryId
                              }).ToList()
                          }).ToList();
            return users;
        }

        public IEnumerable<UserDto> GetUserInFactorRole()
        {
            var users = _userManager.GetUsersInRoleAsync(RoleConstants.Factor).Result
                          .Select(u => new UserDto
                          {
                              Id = u.Id,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              UserType = u.UserType,
                              PhoneNumber = u.PhoneNumber,
                              DisplayName = u.DisplayName,
                              CityId = u.CityId,
                              CreatedDate = u.CreatedDate,
                              Job = u.Job,
                              ProfilePicture = u.ProfilePicture,
                              Email = u.Email
                          }).ToList();
            return users;
        }

        public IEnumerable<ApplicationUser> Search(string query)
        {
            var users = _context.Users
                    .Where(u => u.FirstName.Contains(query) && u.UserType == RoleConstants.Factor)
                    .AsEnumerable()
                    .OrderBy(f => f.FirstName.StartsWith(query, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
                    .ThenBy(f => f.LastName);

            return (IEnumerable<ApplicationUser>)users;
        }

        public async Task<UpdateResult> Update(ProfileDto profileDto)
        {
            var user = await _context.Users.FindAsync(profileDto.Id);

            if (user is null)
                return new UpdateResult { Success = false, ErrorMessages = new List<string> { "User not found." } };

            var now = DateTime.UtcNow;
            var hasChanges = false;

            List<string> errorMessages;
            Dictionary<string, int> daysUntilNextUpdate;

            if (ShouldUpdateProfile(user, profileDto, now, out errorMessages, out daysUntilNextUpdate))
            {
                return new UpdateResult { Success = false, ErrorMessages = errorMessages, DaysUntilNextUpdate = daysUntilNextUpdate };
            }

            // Update user properties
            if (profileDto.FirstName != null)
            {
                user.FirstName = profileDto.FirstName;
                user.LastFirstnameUpdateDate = now; // Update LastFirstnameUpdateDate
                hasChanges = true;
            }

            if (profileDto.LastName != null)
            {
                user.LastName = profileDto.LastName;
                user.LastLastnameUpdateDate = now; // Update LastLastnameUpdateDate
                hasChanges = true;
            }

            if (profileDto.UserType != null)
            {
                if (user.UserType != profileDto.UserType)
                {
                    user.LastUserTypeUpdateDate = now; // Update LastUserTypeUpdateDate
                }
                user.UserType = profileDto.UserType;
                hasChanges = true;

                await UpdateUserTypeAsync(user, profileDto);
            }

            if (profileDto.ProfilePicture != null)
            {
                user.ProfilePicture = await SaveImg(profileDto.ProfilePicture!);
                hasChanges = true;
            }

            if (hasChanges)
            {
                await _context.SaveChangesAsync();
            }

            return new UpdateResult { Success = true, User = user };
        }

        // method to get new role based on UserType
        private string GetNewRoleBasedOnUserType(string userType)
        {
            switch (userType)
            {
                case "Customer":
                    return "Customer";
                case "Factor":
                    return "Factor";
                default:
                    throw new ArgumentException("Invalid UserType", nameof(userType));
            }
        }

        // method to save img
        private async Task<string> SaveImg(IFormFile Img)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Img.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await Img.CopyToAsync(stream);

            return coverName;
        }

        // method to expection not found
        private class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        // method to check if user can update profile or not
        private bool ShouldUpdateProfile(ApplicationUser user, ProfileDto profileDto, DateTime now, out List<string> errorMessages, out Dictionary<string, int> daysUntilNextUpdate)
        {
            errorMessages = new List<string>();
            daysUntilNextUpdate = new Dictionary<string, int>();

            // FirstName and LastName updates
            var lastFirstnameUpdateDate = user.LastFirstnameUpdateDate ?? DateTime.MinValue;
            var daysSinceLastFirstnameUpdate = (now - lastFirstnameUpdateDate).Days;

            var lastLastnameUpdateDate = user.LastLastnameUpdateDate ?? DateTime.MinValue;
            var daysSinceLastLastnameUpdate = (now - lastLastnameUpdateDate).Days;

            if ((profileDto.FirstName != null && user.FirstName != profileDto.FirstName) ||
                (profileDto.LastName != null && user.LastName != profileDto.LastName))
            {
                if (profileDto.FirstName != null && user.FirstName != profileDto.FirstName && daysSinceLastFirstnameUpdate < 10)
                {
                    errorMessages.Add("Firstname can only be changed every 5 days.");
                    daysUntilNextUpdate.Add("Firstname",10 - daysSinceLastFirstnameUpdate);
                }
                if (profileDto.LastName != null && user.LastName != profileDto.LastName && daysSinceLastLastnameUpdate < 10)
                {
                    errorMessages.Add("Lastname can only be changed every 5 days.");
                    daysUntilNextUpdate.Add($"Lastname", 10 - daysSinceLastLastnameUpdate);
                }
            }

            // UserType updates
            var lastUserTypeUpdateDate = user.LastUserTypeUpdateDate ?? DateTime.MinValue;
            var daysSinceLastUserTypeUpdate = (now - lastUserTypeUpdateDate).Days;

            if (profileDto.UserType != null && user.UserType != profileDto.UserType)
            {
                if (daysSinceLastUserTypeUpdate < 30)
                {
                    errorMessages.Add("UserType can only be changed every 15 days.");
                    daysUntilNextUpdate.Add("UserType", 30 - daysSinceLastUserTypeUpdate);
                }
            }

            return errorMessages.Count > 0;
        }

        // method to update user
        private async Task UpdateUserTypeAsync(ApplicationUser user, ProfileDto profileDto)
        {
            if (user.UserType != profileDto.UserType)
            {
                // Remove old role
                var oldRole = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, oldRole);

                // Add new role based on UserType
                var newRole = GetNewRoleBasedOnUserType(profileDto.UserType.ToString());
                await _userManager.AddToRoleAsync(user, newRole);
            }

            if (profileDto.Job != null)
            {
                user.Job = profileDto.Job;
            }

            // Handle job field based on UserType
            if (profileDto.UserType == RoleConstants.Factor)
            {
                if (string.IsNullOrEmpty(user.Job))
                {
                    throw new ValidationException("Job is required for Factor UserType");
                }
            }
            else if (profileDto.UserType == RoleConstants.Customer)
            {
                // Delete job for Customer UserType
                user.Job = null;

                // Check if user has problems and delete if true
                if (user.Problems.Any())
                {
                    _context.Problems.RemoveRange(user.Problems);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
