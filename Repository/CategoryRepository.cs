using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ReadoraProject.Repository
{
    public class CategoryRepository: ICategoryInterface
    {
        

    
        private readonly ReadoraDbContext _context;
        public CategoryRepository(ReadoraDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryDetails>> GetAllAsync()
        {
            return await _context.CategoryDetails.ToListAsync();
        }
        public async Task<IEnumerable<ContentDetails>> GetContentByCategoryIdAsync(int categoryId)
        {
            return await _context.ContentDetails
                            .Where(c => c.CategoryId == categoryId && c.Status == true) // STATUS CHECK ADDED
                            .ToListAsync();
        }
    }
}

