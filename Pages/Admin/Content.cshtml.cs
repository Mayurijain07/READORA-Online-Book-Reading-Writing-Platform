using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.Admin
{
    public class ContentModel : PageModel
    {
        private readonly AdminApiService _service;
        public ContentModel(AdminApiService service)
        {
            _service = service;
        }
        public List<ContentViewModel> Contents { get; set; } = new List<ContentViewModel>();
        public async Task OnGetAsync()
        {
            
            Contents = await _service.GetAllContentsAsync();
        }
        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            
            await _service.UpdateStatusAsync(id, true);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteContentAsync(id);
            return RedirectToPage();
        }
    }
}
