using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Services;
using ReadoraProject.Models;

namespace ReadoraProject.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserApiService _userApi;
        public LoginModel(UserApiService userApi)
        {
            _userApi = userApi;
        }
        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var loginResult = await _userApi.LoginAsync(LoginRequest.Username, LoginRequest.Password); 
            if (loginResult == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password."); 
                return Page();
            }
            HttpContext.Session.SetString("UserRole", loginResult.Role);

            HttpContext.Session.SetInt32("UserId", loginResult.UserId);
            HttpContext.Session.SetString("UserName", loginResult.Username); 
            HttpContext.Session.SetString("JWToken", loginResult.Token);
           
        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl); 
            }

            return RedirectToPage("/Dashboard");
        }
    }
}

