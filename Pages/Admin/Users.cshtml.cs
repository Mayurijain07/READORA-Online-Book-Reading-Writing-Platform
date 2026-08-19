using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly AdminApiService _api;
        public List<UserDetails> Users { get; set; } = new();

        public UsersModel(AdminApiService api) { _api = api; }

        public async Task OnGetAsync()
        {
            
            Users = await _api.GetAllUsersAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _api.DeleteUserAsync(id);
            if (result)
            {
                TempData["Success"] = "User successfully removed from platform.";
            }
            return RedirectToPage();
        }
    }
}

