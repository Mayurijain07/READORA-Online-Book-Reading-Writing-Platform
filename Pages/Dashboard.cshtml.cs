using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IContentInterface _repo; // Repository add ki
        public string? Username { get; set; }
        public string? UserRole { get; set; }
        public List<ContentDetails> MyContents { get; set; } = new();

      public DashboardModel(IContentInterface repo)
        {
            _repo = repo;
        }
       
        public IActionResult OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }
            Username = HttpContext.Session.GetString("UserName");
            UserRole = HttpContext.Session.GetString("UserRole");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
            MyContents = _repo.GetPublishedContentByUserId(userId.Value).ToList();
            }

            return Page();
        }
    }

}
