using ReadoraProject.Models;
using ReadoraProject.Repository;
using ReadoraProject.Interface;
namespace ReadoraProject.Services
{
    public class ProfileApiService
    {
        private readonly IProfileInterface _repo;
        private readonly IWebHostEnvironment _env;

        public ProfileApiService(IProfileInterface repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<ProfileDetails> GetProfile(int userId) => await _repo.GetByUserIdAsync(userId);

        public async Task ProcessProfileUpdate(int userId, ProfileDetails input, IFormFile? photo)
        {
            var existing = await _repo.GetByUserIdAsync(userId);

            // Image Handling
            if (photo != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                input.ProfilePhoto = "/uploads/" + fileName;
            }

            if (existing == null)
            {
                input.UserId = userId;
                input.LastUpdated = DateTime.Now;
                await _repo.CreateAsync(input);
            }
            else
            {
                existing.Bio = input.Bio;
                existing.FullName = input.FullName;
                if (input.ProfilePhoto != null) existing.ProfilePhoto = input.ProfilePhoto;
                existing.LastUpdated = DateTime.Now;
                await _repo.UpdateAsync(existing);
            }
        }

        public async Task DeleteUserProfile(int userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            if (profile != null) await _repo.DeleteAsync(profile.ProfileId);
        }
    }
}

      
        