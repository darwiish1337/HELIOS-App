namespace WebApimyServices.Controllers
{
    /// <summary>
    /// Controller for managing addresses
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressController"/> class.
        /// </summary>
        /// <param name="context">The application database context</param>
        public AddressController(ApplicationDbContext context) => _context = context;

        /// <summary>
        /// Retrieves a list of all governorates in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a paged list of governorates, including their IDs, Arabic names, and English names.
        /// </remarks>
        /// <response code="200">List of governorates successfully retrieved.</response>
        /// <response code="500">An error occurred while retrieving governorates.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllGovers()
        {
            var data = await _context.Governorates
                .Select(a => new GoverCityDto
                {
                    Id = a.Id,
                    NameAR = a.GoverNameAR,
                    NameEN = a.GoverNameEN
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Retrieves a list of cities associated with a specific governorate.
        /// </summary>
        /// <param name="id">The ID of the governorate for which to retrieve cities.</param>
        /// <remarks>
        /// This endpoint returns a list of cities, including their IDs, Arabic names, and English names, 
        /// that are associated with the specified governorate.
        /// </remarks>
        /// <response code="200">List of cities successfully retrieved.</response>
        /// <response code="404">Governorate not found.</response>
        /// <response code="500">An error occurred while retrieving cities.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoverCities(int id)
        {
            var data = await _context.Cities
                .Where(c => c.GovernorateId == id)
                .Select(a => new GoverCityDto
                {
                    Id = a.Id,
                    NameAR=a.CityNameAR,
                    NameEN = a.CityNameEN
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
