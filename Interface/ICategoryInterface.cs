using ReadoraProject.Models;

namespace ReadoraProject.Interface
{
    public interface ICategoryInterface
    {
        Task<IEnumerable<CategoryDetails>> GetAllAsync();
        Task<IEnumerable<ContentDetails>> GetContentByCategoryIdAsync(int categoryId);
    }
}
