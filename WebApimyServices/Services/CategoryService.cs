namespace WebApimyServices.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CategoryForProblemAndUserDto> GetCategories()
        {
            var categories = _context.Categories
               .Select(c => new CategoryForProblemAndUserDto
               {
                   Id = c.Id,
                   NameAR = c.NameAR,
                   NameEN = c.NameEN,
                   Problems = c.Problems.Select(p => new ProblemForCategoryDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       Status = p.Status,
                       ProblemImg = p.ProblemImg,
                       CreatedDate = p.CreatedDate,
                       CategoryId = p.CategoryId,
                       User = new UserForGetProblemDto
                       {
                           Id = p.User.Id,
                           FirstName = p.User.FirstName,
                           LastName = p.User.LastName,
                           DisplayName = p.User.DisplayName,
                           ProfilePicture = p.User.ProfilePicture
                       }
                   }).ToList()
               })
               .ToList();

            return categories;
        }
        public async Task<IEnumerable<CategoryForProblemAndUserDto>> GetProblemByCategoryId(int id)
        {
            var categories = await _context.Categories
               .Where(c => c.Id == id)
               .Select(c => new CategoryForProblemAndUserDto
               {
                   Id = c.Id,
                   NameAR = c.NameAR,
                   NameEN = c.NameEN,
                   Problems = c.Problems.Select(p => new ProblemForCategoryDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       Status = p.Status,
                       ProblemImg = p.ProblemImg,
                       CreatedDate = p.CreatedDate,
                       CategoryId = p.CategoryId,
                       User = new UserForGetProblemDto
                       {
                           Id = p.User.Id,
                           FirstName = p.User.FirstName,
                           LastName = p.User.LastName,
                           DisplayName = p.User.DisplayName,
                           ProfilePicture = p.User.ProfilePicture
                       }
                   }).ToList()
               })
               .ToListAsync();

            return categories;
        }
    }
}
