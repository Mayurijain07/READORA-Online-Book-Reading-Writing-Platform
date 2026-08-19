using ReadoraProject.Interface;
using ReadoraProject.Models;
using ReadoraProject.Repository;

namespace ReadoraProject.Services
{
    public class ContentApiService
    {
        private readonly IContentInterface _repo;
        private readonly IWebHostEnvironment _env;

        public ContentApiService(IContentInterface repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }
        public async Task SaveContentAsync(ContentDetails content, IFormFile? file)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                content.CoverImage = "/uploads/" + fileName;
            }

            content.ContentDate = DateTime.Now;
            content.ContentTime = DateTime.Now.TimeOfDay;

            if (content.ContentId > 0)
            {
                // Purana draft hai, update karo
                await _repo.UpdateContentAsync(content);
            }
            else
            {
                // Naya content hai, add karo
                await _repo.AddContentAsync(content);
            }
        }
        public async Task<ContentDetails?> GetContentAsync(int id)
        {
            return await _repo.GetContentByIdAsync(id);
        }
        public async Task DeleteContentAsync(int id)
        {
            await _repo.DeleteContentAsync(id);
        }
    }
}
