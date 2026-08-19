using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Repository;
using ReadoraProject.Interface;
using ReadoraProject.Services;

namespace ReadoraProject.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly ProfileApiService _service;
        public EditProfileModel(ProfileApiService service) { _service = service; }

        [BindProperty] public ProfileDetails Profile { get; set; } = new();
        public string FullName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            FullName = HttpContext.Session.GetString("UserName") ?? "User";
            var data = await _service.GetProfile(userId.Value);
            if (data != null) Profile = data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? photo)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            await _service.ProcessProfileUpdate(userId.Value, Profile, photo);
            return RedirectToPage("/UserProfile");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null) await _service.DeleteUserProfile(userId.Value);
            return RedirectToPage("/UserProfile");
        }
    }


}

