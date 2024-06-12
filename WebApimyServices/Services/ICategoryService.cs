namespace WebApimyServices.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryForProblemAndUserDto> GetCategories();
        Task<IEnumerable<CategoryForProblemAndUserDto>> GetProblemByCategoryId(int id);
    }
}
