namespace WebApimyServices.Services
{
    public interface IProblemService
    {
         Task Create(ProblemsDto problemsDto);
         Task<Problems?> Update(ProblemsUpdateDto problemsDto);
         Task<IEnumerable<ProblemWithUserWithCategoryDto>> SearchAsync(string query);
         Task<IEnumerable<ProblemsDto>> GetProblemsAsync();
         Task<IEnumerable<ProblemsDto>> GetProblemsByIdAsync(int id);
    }
}
