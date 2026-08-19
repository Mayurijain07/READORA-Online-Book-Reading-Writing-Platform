using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Services;
using ReadoraProject.Models;
using ReadoraProject.Interface;

namespace ReadoraProject.Pages
{
    public class SupportModel : PageModel
    {
        private readonly ISupportService _supportApiService;

        public SupportModel(ISupportService supportApiService)
        {
            _supportApiService = supportApiService;
        }

        [BindProperty]
        public FeedbackDetails Feedback { get; set; } 

        [BindProperty]
        public QueryDetails Query { get; set; } 
        public List<QueryDetails> UserQueries { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var result = await _supportApiService.GetUserQueriesAsync(userId.Value);
            UserQueries = result.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostSubmitFeedbackAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null && Feedback != null)
            {
                Feedback.UserId = userId.Value;
                Feedback.Date = DateTime.Now; 

                await _supportApiService.AddFeedbackAsync(Feedback);
                TempData["Success"] = "Feedback submitted successfully!"; 
            }
            return RedirectToPage("Support");
        }
        public async Task<IActionResult> OnPostSubmitQueryAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (Query == null || string.IsNullOrEmpty(Query.QueryTitle))
            {
                TempData["Error"] = "Please fill all fields.";
                return RedirectToPage("Support");
            }

            if (userId != null)
            {
                Query.UserId = userId.Value;
                Query.QueryDate = DateTime.Now;
                Query.QueryStatus = false; 

                await _supportApiService.AddQueryAsync(Query);
                TempData["Success"] = "Query submitted successfully!";
            }
            return RedirectToPage("Support");
        }
    }
}
