using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReadoraProject.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Date { get; set; }

        public void OnGet()
        {
          
            Name = HttpContext.Session.GetString("AdminName") ?? "Mayuri";
            Role = HttpContext.Session.GetString("Role") ?? "SuperAdmin";
            Date = "2026-03-08"; 
        }
    }
}
