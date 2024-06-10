namespace WebApimyServices.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        IEnumerable<Category> GetCategories();
        Task<IEnumerable<CategoryDto>> GetCategoriesByIdAsync(int id);
        Task<IEnumerable<Problems>> GetProblemsForCategoryAsync(int categoryId);
    }
}
