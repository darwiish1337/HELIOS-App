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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all categories with their associated problems.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of categories, each containing a list of problems.
        /// </remarks>
        /// <response code="200">Returns a list of categories with their associated problems.</response>
        [HttpGet]
        public IActionResult GetCategoriesWithProblem()
        {
            var results = _categoryService.GetCategories();

            return Ok(results);
        }

        /// <summary>
        /// Retrieves all problems associated with a specific category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve problems for.</param>
        /// <remarks>
        /// This endpoint returns a list of problems that belong to the specified category.
        /// </remarks>
        /// <response code="200">Returns a list of problems associated with the specified category.</response>
        /// <response code="404">Returns if no problems are found for the specified category.</response>
        [HttpGet]
        public async Task<ActionResult> GetProblemByCategoryId(int id)
        {
            var results = await _categoryService.GetProblemByCategoryId(id);

            if (!results.Any())
                return NotFound();

            return Ok(results);
        }
    }
}
