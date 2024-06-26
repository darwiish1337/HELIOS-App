namespace WebApimyServices.Controllers
{
    /// <summary>
    /// API controller responsible for managing job-related operations.
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsController"/> class.
        /// </summary>
        /// <param name="context">The database context for interacting with jobs data.</param>
        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all jobs available in the database.
        /// </summary>
        /// <returns>A list of all jobs with basic details (Id, Name, ImagePath).</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var data = await _context.Jobs
                .Select(a => new JobOnlyDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImagePath = a.ImagePath
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Retrieves a specific job by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The ID of the job to retrieve.</param>
        /// <returns>The job details (Id, Name, ImagePath) if found; otherwise, returns NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _context.Jobs
                .Where(j => j.Id == id)
                .Select(a => new JobOnlyDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImagePath = a.ImagePath
                })
                .FirstOrDefaultAsync();

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }
    }
}
