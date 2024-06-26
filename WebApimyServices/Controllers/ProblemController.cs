namespace WebApimyServices.Controllers
{
    /// <summary>
    /// Controller for managing problems, including creating, reading, updating, and deleting problems.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for problem management, and requires authentication and authorization to access its features.
    /// </remarks>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProblemController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProblemController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IProblemService _problemService;

        public ProblemController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogger<ProblemController> logger, ApplicationDbContext context, IProblemService problemService)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _context = context;
            _problemService = problemService;
        }

        /// <summary>
        /// Creates a new problem.
        /// </summary>
        /// <param name="problemsDto">The problem details to be created.</param>
        /// <remarks>
        /// This endpoint creates a new problem with the provided details.
        /// The request must be authenticated and the user must have the required role to perform this action.
        /// </remarks>
        /// <response code="200">Returns the created problem details.</response>
        /// <response code="400">Returns an error if the request is invalid.</response>
        [HttpPost]
        [AuthorizeRole(RoleConstants.Customer, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> AddProblem([FromForm]ProblemsDto problemsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            await _problemService.Create(problemsDto);

            return Ok(problemsDto);
        }

        /// <summary>
        /// Updates an existing problem.
        /// </summary>
        /// <param name="problemsDto">The updated problem data.</param>
        /// <remarks>
        /// This endpoint updates an existing problem with the provided data.
        /// Only authorized users with the "GlobalVerbRolePolicy" policy can access this endpoint.
        /// </remarks>
        /// <response code="200">Returns the updated problem.</response>
        /// <response code="400">Returns if the model state is invalid or the update operation fails.</response>
        [HttpPut]
        [AuthorizeRole(RoleConstants.Customer, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> UpdateProblem(ProblemsUpdateDto problemsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToArray());

            var problem = await _problemService.Update(problemsDto);

            if (problem is null) 
                return BadRequest();

            return Ok(problem);
        }

        /// <summary>
        /// Deletes a problem by ID.
        /// </summary>
        /// <param name="id">The ID of the problem to delete.</param>
        /// <remarks>
        /// This endpoint deletes a problem with the specified ID.
        /// Only authorized users with the "GlobalVerbRolePolicy" policy and the "Customer" role can access this endpoint.
        /// </remarks>
        /// <response code="200">Returns a success message indicating the problem has been deleted.</response>
        /// <response code="403">Returns if the user is not in the "Customer" role.</response>
        /// <response code="400">Returns if the ID is null.</response>
        [HttpDelete("{id}")]
        [AuthorizeRole(RoleConstants.Customer, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> DeleteProblemById(int id)
        {
            var result = await _problemService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a problem by status (false).
        /// </summary>
        /// <param name="id">The ID of the problem to delete.</param>
        /// <remarks>
        /// This endpoint deletes a problem with the specified ID if its status is false.
        /// Only authorized users with the "GlobalVerbRolePolicy" policy can access this endpoint.
        /// </remarks>
        /// <response code="200">Returns a success message indicating the problem has been stopped and will be deleted.</response>
        /// <response code="400">Returns if the problem is still running.</response>
        [HttpDelete]
        [AuthorizeRole(RoleConstants.Customer, unauthorizedMessage: "Please log in to continue", forbiddenMessage: "You do not have the necessary role to access this resource")] // Apply the custom attribute with custom messages
        public async Task<IActionResult> DeleteByStatus(int id)
        {
            var problem = _context.Problems.FirstOrDefault(x => x.Id == id);

            if(problem.Status != false)
            {
                _context.Problems.Remove(problem);
                _context.SaveChanges();

                return Ok("Problem has been stoped! and will be deleted.");
            }

            return BadRequest("Problem Still Running !");
        }

        /// <summary>
        /// Searches for problems by query.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <remarks>
        /// This endpoint searches for problems that match the specified query.
        /// </remarks>
        /// <response code="200">Returns a list of problems that match the search query.</response>
        /// <response code="404">Returns if no problems are found that match the search query.</response>
        [HttpGet("{query}")]
        public async Task<IActionResult> SearchProblems(string query)
        {
            var results = await _problemService.SearchAsync(query);

            if (!results.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }
    }
}
