namespace WebApimyServices.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProblemService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathProblems}";
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(ProblemsDto problemsDto)
        {
            var coverName = await SaveImg(problemsDto.ProblemImg);

            var problem = new Problems
            {
                Name = problemsDto.Name,
                Description = problemsDto.Description,
                ProblemImg = coverName,
                UserId = problemsDto.UserId,
                CategoryId = problemsDto.CategoryId,
            };

            await _context.AddAsync(problem);
            _context.SaveChanges();
        }

        public IEnumerable<ProblemSharedDto> GetProblemsAsync()
        {
            var problems = _context.Problems
               .Select(p => new ProblemSharedDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   Status = p.Status,
                   ProblemImg = p.ProblemImg,
                   CreatedDate = p.CreatedDate,
                   UserId= p.UserId,
                   CategoryId = p.CategoryId,
                   Category = new CategoryForProblemDto
                   {
                       Id = p.Category.Id,
                       NameAR = p.Category.NameAR,
                       NameEN = p.Category.NameEN
                   },
                   User = new UserForGetProblemDto
                   {
                       Id = p.User.Id,
                       FirstName = p.User.FirstName,
                       LastName = p.User.LastName,
                       DisplayName = p.User.DisplayName,
                       ProfilePicture = p.User.ProfilePicture,
                   }
               })
               .ToList();

            return problems;
        }

        public async Task<IEnumerable<ProblemSharedDto>> GetProblemsByIdAsync(int id)
        {
            var problems = await _context.Problems
               .Where(c => c.Id == id)
               .Select(p => new ProblemSharedDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   Status = p.Status,
                   ProblemImg = p.ProblemImg,
                   CreatedDate = p.CreatedDate,
                   UserId = p.UserId,
                   CategoryId = p.CategoryId,
                   Category = new CategoryForProblemDto
                   {
                       Id = p.Category.Id,
                       NameAR = p.Category.NameAR,
                       NameEN = p.Category.NameEN
                   },
                   User = new UserForGetProblemDto
                   {
                       Id = p.User.Id,
                       FirstName = p.User.FirstName,
                       LastName = p.User.LastName,
                       DisplayName = p.User.DisplayName,
                       ProfilePicture = p.User.ProfilePicture,
                   }
               })
               .ToListAsync();

            return problems;
        }

        public async Task<IEnumerable<ProblemSearchDto>> SearchAsync(string query)
        {
            var problems = await _context.Problems
             .Where(x => x.Name.Contains(query) || x.Description.Contains(query))
             .OrderBy(c => c.CreatedDate)
             .Select(u => new ProblemSearchDto
             {
                 Id = u.Id,
                 Name = u.Name,
                 Description = u.Description,
                 Status = u.Status,
                 ProblemImg = u.ProblemImg,
                 CreatedDate = u.CreatedDate,
                 CategoryId = u.CategoryId,
                 UserId=u.UserId,
                 Category = new CategoryDto
                 {
                     Id = u.CategoryId,
                     NameAR = u.Category.NameAR,
                     NameEN = u.Category.NameEN
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

            if (problemsDto.Name != null)
                problem.Name = problemsDto.Name;

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
