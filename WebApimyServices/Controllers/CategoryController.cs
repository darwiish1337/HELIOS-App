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

        // Get All Categories With Problems
        [HttpGet]
        public IActionResult GetCategoriesWithProblem()
        {
            var results = _categoryService.GetCategories();

            return Ok(results);
        }

        // Get All Categories By Id
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
