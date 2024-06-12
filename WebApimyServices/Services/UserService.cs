﻿namespace WebApimyServices.Services
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

        public IEnumerable<UserDto> GetUsersWithProblems()
        {
            var users =  _context.Users
                        .Where(e => e.EmailConfirmed)
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

        public IEnumerable<ApplicationUser> Search(string query)
        {
            var users = _context.Users
                    .Where(u => u.FirstName.Contains(query) && u.UserType == RoleConstants.Factor)
                    .AsEnumerable()
                    .OrderBy(f => f.FirstName.StartsWith(query, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
                    .ThenBy(f => f.LastName);

            return (IEnumerable<ApplicationUser>)users;
        }

        public async Task<ApplicationUser?> Update(ProfileDto profileDto)
        {
            var user = _context.Users.Find(profileDto.Id);

            if (user is null)
                return null;

            if (profileDto.FirstName != null)
                user.FirstName = profileDto.FirstName;

            if (profileDto.LastName != null)
                user.LastName = profileDto.LastName;

            if (profileDto.UserType != null)
            {
                user.UserType = profileDto.UserType; // Update UserType

                if (user.UserType != profileDto.UserType) // Check if UserType has changed
                {
                    // Remove old role
                    var oldRole = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, oldRole);

                    // Add new role based on UserType
                    var newRole = GetNewRoleBasedOnUserType(profileDto.UserType.ToString());
                    await _userManager.AddToRoleAsync(user, newRole);
                }

                if (profileDto.Job != null)
                    user.Job = profileDto.Job;

                // Handle job field based on UserType
                if (profileDto.UserType == RoleConstants.Factor)
                {
                    if (string.IsNullOrEmpty(user.Job))
                    {
                        // Job is required for Factor UserType
                        throw new ValidationException("Job is required for Factor UserType");
                    }
                }
                else if (profileDto.UserType == RoleConstants.Customer)
                {
                    user.Job = null; // Delete job for Customer UserType
                }
            }

            if (profileDto.ProfilePicture != null)
            {
                var currentCover = user.ProfilePicture;
                if (currentCover != null && _imagesPath != null)
                {
                    user.ProfilePicture = await SaveImg(profileDto.ProfilePicture!);
                    var cover = Path.Combine(_imagesPath, currentCover);
                    File.Delete(cover);
                }
                else
                {
                    user.ProfilePicture = await SaveImg(profileDto.ProfilePicture!);
                }
            }

            var effectedRows = await _context.SaveChangesAsync();

            if (effectedRows > 0)
            {
                return user;
            }
            else
            {
                return null;
            }
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

        private async Task<string> SaveImg(IFormFile Img)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Img.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await Img.CopyToAsync(stream);

            return coverName;
        }

        private class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

    }
}
