using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Services
{
    public class CategoryApiService
    {
        private readonly ICategoryInterface _repo;
        public CategoryApiService(ICategoryInterface repo) => _repo = repo;

        public async Task<IEnumerable<CategoryDetails>> GetAllCategoriesAsync()
        {
            return await _repo.GetAllAsync();

        }
        //browse
        public async Task<IEnumerable<ContentDetails>> GetStoriesByCategory(int categoryId)
        {
            return await _repo.GetContentByCategoryIdAsync(categoryId);
        }
    }
}
