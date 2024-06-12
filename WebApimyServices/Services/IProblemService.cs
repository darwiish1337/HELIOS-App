namespace WebApimyServices.Services
{
    public interface IProblemService
    {
         Task Create(ProblemsDto problemsDto);
         Task<Problems?> Update(ProblemsUpdateDto problemsDto);
         Task<IEnumerable<ProblemSearchDto>> SearchAsync(string query);
         IEnumerable<ProblemSharedDto> GetProblemsAsync();
         Task<IEnumerable<ProblemSharedDto>> GetProblemsByIdAsync(int id);
    }
}
