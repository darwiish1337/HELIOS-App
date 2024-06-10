namespace WebApimyServices.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all govers
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

         // Get cities by goverId
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
