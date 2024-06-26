namespace WebApimyServices.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<CategoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IEnumerable<CategoryForProblemAndUserDto> GetCategories()
        {
            var categories = _context.Categories
               .Select(c => new CategoryForProblemAndUserDto
               {
                   Id = c.Id,
                   Name = c.Name,
                   ImagePath = c.ImagePath,
                   Problems = c.Problems.Select(p => new ProblemForCategoryDto
                   {
                       Id = p.Id,
                       Description = p.Description,
                       Status = p.Status,
                       ProblemImg = p.ProblemImg,
                       CreatedDate = p.CreatedDate,
                       CategoryId = p.CategoryId,
                       User = new UserForGetProblemDto
                       {
                           Id = p.User.Id,
                           PhoneNumber = p.User.PhoneNumber,
                           DisplayName = p.User.DisplayName,
                           ProfilePicture = p.User.ProfilePicture
                       }
                   }).ToList()
               })
               .ToList();

            return categories;
        }

        public async Task<List<CategoryForProblemAndUserDto>> GetCategoriesWithProblemsAsyncId(int? cityId, int? governorateId, string userEmail, int id)
        {
            try
            {
                // Retrieve current user's details based on userEmail
                var user = await _context.Users
                    .Include(u => u.City)
                    .ThenInclude(c => c.Governorate)
                    .FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    _logger.LogWarning($"User with email {userEmail} not found.");
                    return null; // Handle this case based on your application's requirements
                }

                // Determine which cityId and governorateId to use
                int selectedCityId = cityId ?? user.CityId;
                int selectedGovernorateId = governorateId ?? user.City?.GovernorateId ?? 0;

                // Retrieve categories with associated problems based on selected cityId and governorateId
                IQueryable<Category> categoriesQuery = _context.Categories
                    .Where(c => c.Id == id)
                    .Include(c => c.Problems)
                    .ThenInclude(p => p.User)
                    .ThenInclude(u => u.City)
                    .ThenInclude(ct => ct.Governorate);

                // Filter categories based on selected cityId and governorateId
                categoriesQuery = categoriesQuery
                    .Where(c => c.Problems.Any(p => p.User.CityId == selectedCityId));

                if (governorateId != null)
                {
                    categoriesQuery = categoriesQuery
                        .Where(c => c.Problems.Any(p => p.User.City.GovernorateId == selectedGovernorateId));
                }

                var categories = await categoriesQuery
                    .Select(c => new CategoryForProblemAndUserDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ImagePath = c.ImagePath,
                        Problems = c.Problems.Select(p => new ProblemForCategoryDto
                        {
                            Id = p.Id,
                            Description = p.Description,
                            Status = p.Status,
                            ProblemImg = p.ProblemImg,
                            CreatedDate = p.CreatedDate,
                            CategoryId = p.CategoryId,
                            User = new UserForGetProblemDto
                            {
                                Id = p.User.Id,
                                DisplayName = p.User.DisplayName,
                                PhoneNumber = p.User.PhoneNumber,
                                ProfilePicture = p.User.ProfilePicture
                            }
                        }).ToList()
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories with problems.");
                throw; // Handle or re-throw the exception based on your application's error handling strategy
            }
        }

        public async Task<List<CategoryForProblemAndUserDto>> GetCategoriesWithProblemsAsync(int? cityId, int? governorateId, string userEmail)
        {
            try
            {
                // Retrieve current user's details based on userEmail
                var user = await _context.Users
                    .Include(u => u.City)
                    .ThenInclude(c => c.Governorate)
                    .FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    _logger.LogWarning($"User with email {userEmail} not found.");
                    return null; // Handle this case based on your application's requirements
                }

                // Determine which cityId and governorateId to use
                int selectedCityId = cityId ?? user.CityId;
                int selectedGovernorateId = governorateId ?? user.City?.GovernorateId ?? 0;

                // Retrieve categories with associated problems based on selected cityId and governorateId
                IQueryable<Category> categoriesQuery = _context.Categories
                    .Include(c => c.Problems)
                    .ThenInclude(p => p.User)
                    .ThenInclude(u => u.City)
                    .ThenInclude(ct => ct.Governorate);

                // Filter categories based on selected cityId and governorateId
                categoriesQuery = categoriesQuery
                    .Where(c => c.Problems.Any(p => p.User.CityId == selectedCityId));

                if (governorateId != null)
                {
                    categoriesQuery = categoriesQuery
                        .Where(c => c.Problems.Any(p => p.User.City.GovernorateId == selectedGovernorateId));
                }

                var categories = await categoriesQuery
                    .Select(c => new CategoryForProblemAndUserDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ImagePath = c.ImagePath,
                        Problems = c.Problems.Select(p => new ProblemForCategoryDto
                        {
                            Id = p.Id,
                            Description = p.Description,
                            Status = p.Status,
                            ProblemImg = p.ProblemImg,
                            CreatedDate = p.CreatedDate,
                            CategoryId = p.CategoryId,
                            User = new UserForGetProblemDto
                            {
                                Id = p.User.Id,
                                DisplayName = p.User.DisplayName,
                                PhoneNumber = p.User.PhoneNumber,
                                ProfilePicture = p.User.ProfilePicture
                            }
                        }).ToList()
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories with problems.");
                throw; // Handle or re-throw the exception based on your application's error handling strategy
            }
        }
    }
}
