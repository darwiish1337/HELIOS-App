namespace WebApimyServices.Controllers
{
    /// <summary>
    /// Controller for managing users
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for user-related operations, such as retrieving user information and managing user roles.
    /// </remarks>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Searches for users based on a search query.
        /// </summary>
        /// <param name="query">The search query to search for users.</param>
        /// <remarks>
        /// This endpoint searches for users based on the provided query and returns a list of matching users.
        /// </remarks>
        /// <response code="200">List of matching users successfully retrieved.</response>
        /// <response code="404">No users found matching the search query.</response>
        [HttpGet("{query}")]
        public ActionResult<IEnumerable<ApplicationUser>> Search(string query)
        {
            var results = _userService.Search(query);

            if (!results.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        /// <summary>
        /// Updates a user's profile information.
        /// </summary>
        /// <param name="profileDto">The updated profile information.</param>
        /// <remarks>
        /// This endpoint updates a user's profile information based on the provided <see cref="ProfileDto"/>.
        /// </remarks>
        /// <response code="200">User profile updated successfully.</response>
        /// <response code="400">Invalid request. Please check the request body for errors.</response>
        /// <response code="404">User not found or unable to update profile.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(ProfileDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var user = await _userService.Update(profileDto);

            if (user is null)
                return BadRequest();

            return Ok(user);
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="changePasswordDto">The change password request containing the old and new passwords.</param>
        /// <param name="id">The ID of the user whose password is being changed.</param>
        /// <remarks>
        /// This endpoint changes a user's password based on the provided <see cref="ChangePasswordDto"/>.
        /// </remarks>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Invalid request. Please check the request body for errors.</response>
        /// <response code="400">Failed to change password. Please try again.</response>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto, string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var result = await _userService.ChangePassowrd(changePasswordDto, id);

            if (result != true)
            {
                return BadRequest("Feild To Change Password !");
            }

            return Ok("Password Has Been Changed !");
        }

        /// <summary>
        /// Retrieves a list of users in the Factor role.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of users who are assigned to the Factor role.
        /// </remarks>
        /// <response code="200">Returns a list of users in the Factor role.</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsersInFactorRole()
        {
            var users = await _userManager.GetUsersInRoleAsync(RoleConstants.Factor);

            return Ok(users);
        }

        /// <summary>
        /// Retrieves a list of users with problems.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of users who have unresolved problems or issues.
        /// </remarks>
        /// <response code="200">Returns a list of users with problems.</response>
        /// <response code="404">No users with problems found.</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersWithProblems()
        {
            var users = _userService.GetUsersWithProblems();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}
