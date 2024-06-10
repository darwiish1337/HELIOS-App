namespace WebApimyServices.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        // Search Factor's phonebook
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

        // Update user profile
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

        // Change Password
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

        // Get users in role Factor 
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsersInFactorRole()
        {
            var users = await _userManager.GetUsersInRoleAsync(RoleConstants.Factor);

            return Ok(users);
        }

        // Get Users With Problems
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
