using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly IAdminService _service;
        public ReportsModel(IAdminService service) { _service = service; }

        public DashboardViewModel ReportData { get; set; }

        public async Task OnGetAsync()
        {
           
            ReportData = await _service.GetDashboardStatsAsync();
        }
    }
}
