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
        private readonly IRateService _rateService;

        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager, IRateService rateService)
        {
            _userService = userService;
            _userManager = userManager;
            _rateService = rateService;
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
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetFactors()
        {
            var users = _userService.GetUserInFactorRole();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Retrieves a list of users in the Customer role with their associated problems.
        /// </summary>
        /// <returns>A list of <see cref="UserDto"/> objects, each containing a user's details and their associated problems.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomers()
        {
            var users = _userService.GetUserInCustomerRoleWithProblems();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Adds a new rate for a specific factor by a customer.
        /// Only customers can add rates, and only factors can receive rates.
        /// </summary>
        /// <param name="request">The request object containing the customer ID, factor ID, and rating value.</param>
        /// <returns>A CreatedAtAction result with a message indicating that the rate was added successfully.</returns>
        /// <response code="201">The rate was added successfully.</response>
        /// <response code="400">The request was invalid or the customer or factor ID was not found.</response>
        /// <response code="500">An error occurred while adding the rate.</response>
        [HttpPost]
        public async Task<ActionResult> AddRate([FromBody] AddRateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            try
            {
                await _rateService.AddRateAsync(request.CustomerId, request.FactorId, request.RatingValue);
                return Ok("Rate added successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing rate for a factor and customer.
        /// Only the customer who added the rate can update it, and only for the same factor.
        /// </summary>
        /// <param name="rateId">The ID of the rate to update.</param>
        /// <param name="request">The request object containing the new rating value.</param>
        /// <returns>An OK result with a message indicating that the rate was updated successfully.</returns>
        /// <response code="200">The rate was updated successfully.</response>
        /// <response code="400">The request was invalid or the rate ID was not found.</response>
        /// <response code="500">An error occurred while updating the rate.</response>
        [HttpPut("{rateId}")]
        public async Task<ActionResult> UpdateRate(int rateId, [FromBody] UpdateRateRequestDto request)
        {
            try
            {
                await _rateService.UpdateRateAsync(rateId, request.RatingValue);
                return Ok("Rate updated successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the rate");
            }
        }

        /// <summary>
        /// Deletes a rate for a factor and customer.
        /// Only the customer who added the rate can delete it, and only for the same factor.
        /// </summary>
        /// <param name="rateId">The ID of the rate to delete.</param>
        /// <returns>An OK result with a message indicating that the rate was deleted successfully.</returns>
        /// <response code="200">The rate was deleted successfully.</response>
        /// <response code="400">The request was invalid or the rate ID was not found.</response>
        /// <response code="500">An error occurred while deleting the rate.</response>
        [HttpDelete("{rateId}")]
        public async Task<ActionResult> DeleteRate(int rateId)
        {
            try
            {
                await _rateService.DeleteRateAsync(rateId);
                return Ok("Rate deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the rate");
            }
        }

        /// <summary>
        /// Retrieves all rates for a factor.
        /// </summary>
        /// <param name="factorId">The ID of the factor to retrieve rates for.</param>
        /// <returns>An OK result with a list of rates for the factor.</returns>
        /// <response code="200">The rates were retrieved successfully.</response>
        /// <response code="500">An error occurred while retrieving rates for the factor.</response>
        [HttpGet("{factorId}")]
        [AllowAnonymous]
        public async Task<ActionResult<FactorRatingDto>> GetRatesForFactor(string factorId)
        {
            try
            {
                var rates = await _rateService.GetRatesForFactorAsync(factorId);
                var response = new FactorRatingDto
                {
                    Ratings = rates,
                    RatingCount = rates.Count()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving rates for the factor");
            }
        }

        /// <summary>
        /// Adds or updates the title and description for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="dto">The DTO containing the title and description.</param>
        /// <returns>The updated UserJobInfoDto with the new title and description.</returns>
        /// <response code="200">Returns the updated UserJobInfoDto.</response>
        /// <response code="400">If there is an error processing the request.</response>
        [HttpPost]
        [AuthorizeRole(RoleConstants.Factor, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> AddTitleAndDescriptionForJob(string userId, [FromBody] UserJobInfoDto dto)
        {
            try
            {
                var result = await _userService.Add(userId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the title and description for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="dto">The DTO containing the updated title and description.</param>
        /// <returns>The updated UserJobInfoDto with the new title and description.</returns>
        /// <response code="200">Returns the updated UserJobInfoDto.</response>
        /// <response code="400">If there is an error processing the request.</response>
        [HttpPut]
        [AuthorizeRole(RoleConstants.Factor, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> UpdateTitleAndDescription(string userId, [FromBody] UserJobInfoDto dto)
        {
            try
            {
                var result = await _userService.Update(userId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the title and description for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The number of rows affected (1 if successful, 0 if no changes were made).</returns>
        /// <response code="200">Returns the number of rows affected.</response>
        /// <response code="400">If there is an error processing the request.</response>
        [HttpDelete]
        [AuthorizeRole(RoleConstants.Factor, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> DeleteTitleAndDescription(string userId)
        {
            try
            {
                var rowsAffected = await _userService.Delete(userId);
                return Ok(new { rowsAffected });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves the current logged-in user's information.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the current user's details if found, 
        /// or a "Not Found" response if the user ID is not present in the token or the user does not exist.
        /// </returns>
        /// <remarks>
        /// This method extracts the user ID from the JWT token claims and retrieves the user's details from the database.
        /// Ensure the request contains a valid JWT token with the "uid" claim.
        /// </remarks>
        /// <response code="200">Returns the current user's details.</response>
        /// <response code="404">If the user ID is not found in the token or the user does not exist.</response>
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue("uid"); // Extract user ID from claims

            if (userId == null)
            {
                return NotFound("User ID not found in token");
            }

            var user = await _userManager.Users
                .Include(u => u.City) // Ensure City is included
                .ThenInclude(c => c.Governorate) // Include Governorate if City is included
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Prepare a DTO or an anonymous object to return only necessary data
            var userDto = new
            {
                user.Id,
                user.FirstName, 
                user.LastName,
                user.DisplayName,
                user.ProfilePicture,
                user.UserType,
                user.Email,
                user.PhoneNumber,
                user.CityId,
                user.City?.GovernorateId,
                user.JobId,
                user.Job,
                user.Title,
                user.Description,
                user.CreatedDate,
                user.LastActive,
                user.Problems,
                user.CustomerRates,
                user.ReceivedRates,
                user.AverageRating,
                user.LastFirstnameUpdateDate, 
                user.LastLastnameUpdateDate,
                user.LastUserTypeUpdateDate,
                user.RefreshTokens
            };

            return Ok(userDto);
        }
    }
}
