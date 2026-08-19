using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.Admin
{
    public class QueriesModel : PageModel
    {
        private readonly AdminApiService _api;

        public QueriesModel(AdminApiService api)
        {
            _api = api;
        }

        public List<QueryViewModel> QueryDetails { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToPage("/Admin/AdminLogin");
            }

          
            QueryDetails = await _api.GetAllQueriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostResolve(int id, string message)
        {
            
            await _api.ResolveQueryWithMsgAsync(id, message);

           
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _api.DeleteQueryAsync(id);

            return RedirectToPage();
        }
    }
}