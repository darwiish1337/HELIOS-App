namespace WebApimyServices.Controllers
{
    /// <summary>
    /// Controller for managing categories and their associated problems.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for retrieving and manipulating categories and their problems.
    /// </remarks>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        /// <summary>
        /// Controller for managing categories and associated problems.
        /// </summary>
        /// <param name="categoryService">The service responsible for handling category-related operations.</param>
        /// <param name="logger">Logger instance for logging category-related activities.</param>
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all categories with their associated problems.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of categories, each containing a list of problems.
        /// </remarks>
        /// <response code="200">Returns a list of categories with their associated problems.</response>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCategoriesWithProblem()
        {
            var results = _categoryService.GetCategories();

            return Ok(results);
        }

        /// <summary>
        /// Retrieves categories with associated problems for a specific category ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <param name="cityId">Optional. Filters categories by the specified city ID.</param>
        /// <param name="governorateId">Optional. Filters categories by the specified governorate ID.</param>
        /// <returns>Returns categories with associated problems.</returns>
        /// <response code="200">Returns categories with associated problems.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If no categories are found for the specified ID.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriesWithProblemsById(int id, [FromQuery] int? cityId, [FromQuery] int? governorateId)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

                if (userEmail == null)
                {
                    return Unauthorized(); // Handle unauthorized access based on your application's requirements
                }

                var categories = await _categoryService.GetCategoriesWithProblemsAsyncId(cityId, governorateId, userEmail, id);

                if (categories == null)
                {
                    return NotFound($"No categories found for ID {id}.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching categories with problems for ID {id}.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        /// <summary>
        /// Retrieves categories with associated problems based on optional cityId and governorateId.
        /// </summary>
        /// <param name="cityId">Optional. Filters categories by the specified city ID.</param>
        /// <param name="governorateId">Optional. Filters categories by the specified governorate ID.</param>
        /// <returns>Returns categories with associated problems.</returns>
        /// <response code="200">Returns categories with associated problems.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetCategoriesWithProblems([FromQuery] int? cityId, [FromQuery] int? governorateId)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

                if (userEmail == null)
                {
                    return Unauthorized(); // Handle unauthorized access based on your application's requirements
                }

                var categories = await _categoryService.GetCategoriesWithProblemsAsync(cityId, governorateId, userEmail);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories with problems.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
