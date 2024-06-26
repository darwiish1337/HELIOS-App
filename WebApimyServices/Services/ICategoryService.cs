namespace WebApimyServices.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryForProblemAndUserDto> GetCategories();
        Task<List<CategoryForProblemAndUserDto>> GetCategoriesWithProblemsAsync(int? cityId, int? governorateId, string userEmail);
        Task<List<CategoryForProblemAndUserDto>> GetCategoriesWithProblemsAsyncId(int? cityId, int? governorateId, string userEmail, int id);
    }
}
