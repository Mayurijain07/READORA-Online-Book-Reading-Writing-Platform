using ReadoraProject.Models;
using ReadoraProject.Services;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Runtime.InteropServices;

namespace ReadoraProject.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserApiService _userApi;
        public RegisterModel(UserApiService userApi)
        {
            _userApi = userApi;
        }
        [BindProperty]
        public UserDetails AppUser { get; set; } = new();
       
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("AppUser.UserId");
            ModelState.Remove("AppUser.CreateDate");
           

            if (!ModelState.IsValid)
                return Page();
         

            AppUser.IsActive = true;
            AppUser.CreateDate = DateTime.Now;

            var result = await _userApi.RegisterAsync(AppUser);
            if (result.Success)
            {
                
                TempData["SuccessMessage"] = "Registration Successful!";
                return Page();
            }
          
            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }


        

    }
}