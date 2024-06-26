namespace WebApimyServices.Services
{
    public interface IProblemService
    {
         Task Create(ProblemsDto problemsDto);
         Task<Problems?> Update(ProblemsUpdateDto problemsDto);
         Task<IEnumerable<ProblemSearchDto>> SearchAsync(string query);
         Task<bool> Delete(int id);
    }
}
