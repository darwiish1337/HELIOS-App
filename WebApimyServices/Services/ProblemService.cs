namespace WebApimyServices.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProblemService> _logger;
        private readonly IMapper _mapper;

        public ProblemService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, IMapper mapper, ILogger<ProblemService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathProblems}";
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Create(ProblemsDto problemsDto)
        {
            var coverName = await SaveImg(problemsDto.ProblemImg);

            var problem = new Problems
            {
                Description = problemsDto.Description,
                ProblemImg = coverName,
                UserId = problemsDto.UserId,
                CategoryId = problemsDto.CategoryId,
            };

            await _context.AddAsync(problem);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ProblemSearchDto>> SearchAsync(string query)
        {
            var problems = await _context.Problems
             .Where(x => x.Description.Contains(query))
             .OrderBy(c => c.CreatedDate)
             .Select(u => new ProblemSearchDto
             {
                 Id = u.Id,
                 Description = u.Description,
                 Status = u.Status,
                 ProblemImg = u.ProblemImg,
                 CreatedDate = u.CreatedDate,
                 CategoryId = u.CategoryId,
                 UserId=u.UserId,
                 Category = new CategoryDto
                 {
                     Id = u.CategoryId,
                     Name = u.Category.Name
                 },
                 User = new UserDtoForSearch
                 {
                     Id = u.UserId,
                     DisplayName = u.User.DisplayName,
                     Email = u.User.Email,
                     ProfilePicture = u.User.ProfilePicture,
                     PhoneNumber = u.User.PhoneNumber
                 }
             })
             .ToListAsync();

            return problems;
        }

        public async Task<Problems?> Update(ProblemsUpdateDto problemsDto)
        {
            var problem = _context.Problems.Find(problemsDto.Id);

            if (problem is null)
                return null;

            if (problemsDto.Description != null)
                problem.Description = problemsDto.Description;

            if (problemsDto.Status != null)
                problem.Status = problemsDto.Status;

            if (problemsDto.CategoryId != null)
                problem.CategoryId = problemsDto.CategoryId;

            var currentCover = problem.ProblemImg;

            if (currentCover != null && _imagesPath != null)
            {
                problem.ProblemImg = await SaveImg(problemsDto.ProblemImg!);
                var cover = Path.Combine(_imagesPath, currentCover);
                File.Delete(cover);
            }
            else
            {
                problem.ProblemImg = await SaveImg(problemsDto.ProblemImg!);
            }

            var effectdRows = _context.SaveChanges();

            if (effectdRows > 0)
            {
                return problem;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var problem = await _context.Problems.FindAsync(id);
            if (problem == null)
            {
                return false;
            }

            // Delete image file
            var imagePath = Path.Combine(_imagesPath, problem.ProblemImg);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();

            return true;
        }

        #region Private Methods
        // Save Img
        private async Task<string> SaveImg(IFormFile Img)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Img.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await Img.CopyToAsync(stream);

            return coverName;
        }
        #endregion
    }
}
