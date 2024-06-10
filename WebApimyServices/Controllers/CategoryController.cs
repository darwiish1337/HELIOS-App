namespace WebApimyServices.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Get All Categories
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var results = await _categoryService.GetCategoriesAsync();

            return Ok(results);
        }

        // Get All Categories With Problems
        [HttpGet]
        public IActionResult GetCategoriesWithProblem()
        {
            var results = _categoryService.GetCategories();

            return Ok(results);
        }

        // Get All Categories By Id
        [HttpGet]
        public async Task<ActionResult> GetCategoriesById(int id)
        {
            var results = await _categoryService.GetCategoriesByIdAsync(id);

            if (!results.Any())
                return NotFound();

            return Ok(results);
        }

        // Get Problems With Category
        [HttpGet]
        public async Task<ActionResult> GetProblemWithCategories(int categoryId)
        {
            var result = await _categoryService.GetProblemsForCategoryAsync(categoryId);

            return Ok(result);
        }
    }
}
