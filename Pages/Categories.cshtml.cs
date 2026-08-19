using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages
{
    public class CategoriesModel : PageModel
    {
      
        
            private readonly CategoryApiService _service;
        public IEnumerable<CategoryDetails> Categories { get; set; }

        public CategoriesModel(CategoryApiService service) => _service = service;

        public async Task OnGetAsync()
        {
            Categories = await _service.GetAllCategoriesAsync();
        }
    }
    }

