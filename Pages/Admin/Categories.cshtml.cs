using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.Admin
{
    public class CategoriesModel : PageModel
    {
        private readonly AdminApiService _adminService;

        public CategoriesModel(AdminApiService adminService)
        {
            _adminService = adminService;
        }

        public List<CategoryDetails> Categories { get; set; } = new();

        [BindProperty]
        public CategoryDetails NewCategory { get; set; }

        public async Task OnGetAsync()
        {
          
            Categories = await _adminService.GetAllCategoriesAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!string.IsNullOrEmpty(NewCategory.CategoryName))
            {
               
                await _adminService.AddCategoryAsync(NewCategory);
                TempData["Success"] = "Category Added!";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
          
            await _adminService.DeleteCategoryAsync(id);
            TempData["Success"] = "Category Deleted!";
            return RedirectToPage();
        }
    }
}
  