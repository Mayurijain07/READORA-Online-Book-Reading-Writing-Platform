using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Data;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Pages
{
   
    public class WriteModel : PageModel
    {

        private readonly ReadoraDbContext _context;
        private readonly ContentApiService _service;

        public WriteModel(ReadoraDbContext context, ContentApiService service)
        {
            _context = context;
            _service = service;
        }

        [BindProperty]
        public ContentDetails Content { get; set; }

        public List<CategoryDetails> CategoryList { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            CategoryList = _context.CategoryDetails.ToList();

            if (id.HasValue)
            {
                
                var existingContent = await _service.GetContentAsync(id.Value);
                if (existingContent != null)
                {
                    Content = existingContent;
                }
            }
        }


        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            // 1. Session check
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
         
            string statusValue = Request.Form["status"];
            bool isPublished = (statusValue == "1");

            Content.UserId = userId.Value;
            Content.Status = isPublished; 
            Content.ContentDate = DateTime.Now;
            Content.ContentTime = DateTime.Now.TimeOfDay;

            await _service.SaveContentAsync(Content, file);

            // 5. Redirect based on action
            if (!isPublished)
            {
               
                return RedirectToPage("/UserProfile", new { id = userId });
            }

            return RedirectToPage("/Dashboard");
        }
    }
    }
    

