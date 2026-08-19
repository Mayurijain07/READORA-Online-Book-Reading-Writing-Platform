using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using ReadoraProject.Services;
using System.Reflection.Metadata.Ecma335;

namespace ReadoraProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ReadoraDbContext _context; 
        private readonly IInteractionInterface _interactionService;
        public List<ContentDetails> AllContents { get; set; } = new();

        public IndexModel(ReadoraDbContext context, IInteractionInterface interactionService) {
        _context= context;
        _interactionService = interactionService;
        }
        public string? Username{ get; set; }
        public string ContentType { get; set; } 
        public DateTime CreatedDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }
        public async Task OnGetAsync(string? searchQuery)
        {
            SearchQuery = searchQuery;
            ViewData["CurrentSearch"] = searchQuery;
            Username = HttpContext.Session.GetString("UserName");
            var loggedInUserId = HttpContext.Session.GetInt32("UserId");
            var query = _context.ContentDetails
                                .Include(c => c.Category)
                                .Include(c => c.User) 
                                .Where(c => c.Status == true)
                                .AsQueryable();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(c => c.Title.Contains(searchQuery) ||
                              c.ContentType.Contains(searchQuery) ||
                              (c.Category != null && c.Category.CategoryName.Contains(searchQuery)));
            }
            AllContents = await query.ToListAsync();
            if (loggedInUserId != null)
            {
                foreach (var item in AllContents)
                {
                    item.IsLikedByMe = await _context.LikeDetails
                        .AnyAsync(l => l.UserId == loggedInUserId && l.ContentId == item.ContentId);

                    item.IsFavByMe = await _context.FavouriteDetails
                        .AnyAsync(f => f.UserId == loggedInUserId && f.ContentId == item.ContentId);
                }
            }
        }
        public async Task<IActionResult> OnPostToggleLikeAsync(int contentId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            await _interactionService.ToggleLikeAsync(userId.Value, contentId);
            return RedirectToPage(); // Page reload taaki icon update ho jaye
        }
        public async Task<IActionResult> OnPostToggleFavAsync(int contentId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            await _interactionService.ToggleFavouriteAsync(userId.Value, contentId);
            return RedirectToPage();
        }
    }
}
   

    