using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Repository
{
    public class ContentRepository:IContentInterface
    {
        private readonly ReadoraDbContext _context;
        public ContentRepository(ReadoraDbContext context) => _context = context;

        public async Task AddContentAsync(ContentDetails content)
        {
            _context.ContentDetails.Add(content);
            await _context.SaveChangesAsync();
        }

        // Dashboard ke liye logic
        public IEnumerable<ContentDetails> GetPublishedContentByUserId(int userId)
        {
            return _context.ContentDetails
                           .Where(c => c.UserId == userId && c.Status == true) // Status check
                           .OrderByDescending(c => c.ContentDate)
                           .ToList();
        }

        // Profile page ke liye logic
        public IEnumerable<ContentDetails> GetDraftsByUserId(int userId)
        {
            return _context.ContentDetails
                           .Where(c => c.UserId == userId && c.Status == false) // Draft check
                           .ToList();
        }

        public async Task<ContentDetails?> GetContentByIdAsync(int id)
        {
            return await _context.ContentDetails.FindAsync(id);
        }
        public async Task UpdateContentAsync(ContentDetails content)
        {
            var existing = await _context.ContentDetails.FindAsync(content.ContentId);
            if (existing != null)
            {
                // Data update karein
                existing.Title = content.Title;
                existing.Description = content.Description;
                existing.CategoryId = content.CategoryId;
                existing.ContentType = content.ContentType;
                existing.Status = content.Status; // Published (1) ya Draft (0)
                existing.ContentDate = DateTime.Now;
                existing.ContentTime = DateTime.Now.TimeOfDay;

                if (!string.IsNullOrEmpty(content.CoverImage))
                {
                    existing.CoverImage = content.CoverImage;
                }

                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteContentAsync(int id)
        {
            var content = await _context.ContentDetails.FindAsync(id);
            if (content != null)
            {
                _context.ContentDetails.Remove(content);
                await _context.SaveChangesAsync();
            }
        }
    }
}
