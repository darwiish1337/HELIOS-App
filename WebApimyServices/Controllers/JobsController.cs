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
        /// Retrieves all jobs with associated user details.
        /// </summary>
        /// <returns>A list of all jobs including user details (Id, Name, ImagePath, User).</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllJobsWithUser()
        {
            var data = await _context.Jobs
                .Include(j => j.User)
                .Select(a => new JobDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImagePath = a.ImagePath,
                    User = a.User == null ? null : new FactorForJobDto
                    {
                        Id = a.User.Id,
                        DisplayName = a.User.DisplayName,
                        PhoneNumber = a.User.PhoneNumber,
                        ProfilePicture = a.User.ProfilePicture,
                        Description = a.User.Description,
                        Title = a.User.Title
                    }
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Retrieves a specific job with associated user details by its ID.
        /// </summary>
        /// <param name="id">The ID of the job to retrieve.</param>
        /// <returns>The job details including user details (Id, Name, ImagePath, User) if found; otherwise, returns NotFound.</returns>
        [HttpGet("{id}/with-user")]
        public async Task<IActionResult> GetJobWithUserById(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.User)
                .Where(j => j.Id == id)
                .Select(a => new JobDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImagePath = a.ImagePath,
                    User = a.User == null ? null : new FactorForJobDto
                    {
                        Id = a.User.Id,
                        DisplayName = a.User.DisplayName,
                        PhoneNumber = a.User.PhoneNumber,
                        ProfilePicture = a.User.ProfilePicture,
                        Description = a.User.Description,
                        Title = a.User.Title
                    }
                })
                .FirstOrDefaultAsync();

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
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
