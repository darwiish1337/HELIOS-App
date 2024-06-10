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

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return category != null ? new[] { _mapper.Map<CategoryDto>(category) } : Enumerable.Empty<CategoryDto>();
        }
            
        public async Task<IEnumerable<Problems>> GetProblemsForCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            var problems = await _context.Problems
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            return problems;
        }
    }
}
