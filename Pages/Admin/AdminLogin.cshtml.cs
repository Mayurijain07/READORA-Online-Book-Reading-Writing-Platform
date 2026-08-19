using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly IUserInterface _userRepository;

        public AdminLoginModel(IUserInterface userRepository)
        {
            _userRepository = userRepository;
        }
        [BindProperty]
        public AdminLoginRequest AdminLoginRequest { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var admin = _userRepository.GetAdminDetails(AdminLoginRequest.AdminName, AdminLoginRequest.AdminPassword);

            if (admin == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Admin Credentials.");
                return Page();
            }

            HttpContext.Session.SetInt32("AdminId", admin.AdminId);
            HttpContext.Session.SetString("AdminName", admin.AdminName ?? "Admin");

            return RedirectToPage("/Admin/Dashboard");
        }
    }
}