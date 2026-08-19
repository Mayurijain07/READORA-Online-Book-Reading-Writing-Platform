using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Services;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.admin
{
    public class DashboardModel : PageModel
    {

        private readonly AdminApiService _service;

        public DashboardModel(AdminApiService service)
        {
            _service = service;
        }

       
        public DashboardViewModel Stats { get; set; } = new DashboardViewModel();

        public async Task<IActionResult> OnGetAsync() 
        {
            
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToPage("/Admin/AdminLogin");
            }

           
            Stats = await _service.GetDashboardStatsAsync();

            return Page();
        }
    }
}
