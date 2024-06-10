namespace WebApimyServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class ProblemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProblemService _problemService;

        public ProblemController(ApplicationDbContext context,IProblemService problemService)
        {
            _context = context;
            _problemService = problemService;
        }

        //Add New Problem
        [HttpPost]
        [Authorize(Policy = "GlobalVerbRolePolicy")]
        public async Task<IActionResult> AddProblem([FromForm]ProblemsDto problemsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            await _problemService.Create(problemsDto);

            return Ok(problemsDto);
        }

        // Get All Problems
        [HttpGet]
        public async Task<ActionResult> GetProblems()
        {
            var results = await _problemService.GetProblemsAsync();

            return Ok(results);
        }

        // Get All Problems By Id
        [HttpGet]
        public async Task<ActionResult> GetProblemsById(int id)
        {
            var results = await _problemService.GetProblemsByIdAsync(id);

            if (!results.Any())
                return NotFound(); 

            return Ok(results);
        }

        //Update Current Problem
        [HttpPut]
        [Authorize(Policy = "GlobalVerbRolePolicy")]
        public async Task<IActionResult> UpdateProblem(ProblemsUpdateDto problemsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToArray());

            var problem = await _problemService.Update(problemsDto);

            if (problem is null) 
                return BadRequest();

            return Ok(problem);
        }

        //Delete Problem By ID
        [HttpDelete("{id}")]
        [Authorize(Policy = "GlobalVerbRolePolicy")]
        public async Task<IActionResult> DeleteProblemById(int id)
        {
            if (!User.IsInRole(RoleConstants.Customer))
                return Forbid();

            if (id != null)
            {
                Problems problem = _context.Problems.FirstOrDefault(e => e.Id == id);
                _context.Problems.Remove(problem);
                _context.SaveChanges();
                return Ok("Problem has been deleted !");
            }
            return BadRequest("Is Null !");
        }

        //Delete when status false
        [HttpDelete]
        [Authorize(Policy = "GlobalVerbRolePolicy")]
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

        //Search Problem
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
