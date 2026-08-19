using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages
{
    public class BrowseModel : PageModel
    {
        private readonly CategoryApiService _service;
        public IEnumerable<ContentDetails> Contents { get; set; } = new List<ContentDetails>();

        public BrowseModel(CategoryApiService service) => _service = service;

        public async Task OnGetAsync(int categoryId)
        {
            // Service se data mangwaya
            Contents = await _service.GetStoriesByCategory(categoryId);
        }
    }
}
