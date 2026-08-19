using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IContentInterface _repo;
        private readonly ReadoraDbContext _context; 
        private readonly IInteractionInterface _interactionService; 
        public DetailsModel(IContentInterface repo, ReadoraDbContext context, IInteractionInterface interactionService)
        {
            _repo = repo;
            _context = context;
            _interactionService = interactionService;
        }
        public ContentDetails Content { get; set; } = new();
        public List<CommentDetails> Comments { get; set; } = new(); 
        [BindProperty]
        public string NewCommentText { get; set; } 
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            System.Diagnostics.Debug.WriteLine("Checking session for user: " + userName);

            if (string.IsNullOrEmpty(userName))
            {
                TempData["Message"] = "Please login or register to read the full story!";
                return RedirectToPage("/Login", new { returnUrl = $"/Details?id={id}" });
            }
            Content = await _repo.GetContentByIdAsync(id);
            if (Content == null) return NotFound();
            Comments = await _context.CommentDetails
                .Include(c => c.User) 
                .Where(c => c.ContentId == id)
                .OrderByDescending(c => c.CommentDate) 
                .ToListAsync();
            await _interactionService.AddToHistoryAsync(userId.Value, id);
            
            return Page();
          }
        public async Task<IActionResult> OnPostAddCommentAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            if (!string.IsNullOrWhiteSpace(NewCommentText))
            {
                var comment = new CommentDetails
                {
                    ContentId = id,
                    UserId = userId.Value,
                    CommentText = NewCommentText,
                    CommentDate = DateTime.Now
                };

                await _interactionService.AddCommentAsync(comment);
            }

            return RedirectToPage(new { id = id }); 
        }
        public async Task<IActionResult> OnPostDeleteCommentAsync(int commentId, int contentId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            await _interactionService.DeleteCommentAsync(commentId, userId.Value);

            return RedirectToPage(new { id = contentId });
        }
    }
}
