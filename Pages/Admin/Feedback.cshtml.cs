using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.Admin
{
    public class FeedbackModel : PageModel
    {
        private readonly AdminApiService _api;

        public FeedbackModel(AdminApiService api)
        {
            _api = api;
        }

        public List<FeedbackViewModel> FeedbackDetails { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToPage("/Admin/AdminLogin");
            }

            FeedbackDetails = await _api.GetAllFeedbackAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostMarkRead(int id)
        {
            await _api.MarkReadAsync(id);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _api.DeleteFeedbackAsync(id);
            return RedirectToPage();
        }
    }
}