using ReadoraProject.Interface;
using ReadoraProject.Models;
using ReadoraProject.Data;
using Microsoft.EntityFrameworkCore;

namespace ReadoraProject.Repository
{
    public class ProfileRepository:IProfileInterface
    {
        private readonly ReadoraDbContext _context;
        public ProfileRepository(ReadoraDbContext context) { _context = context; }

        public async Task<ProfileDetails> GetByUserIdAsync(int userId) =>
            await _context.ProfileDetails.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task CreateAsync(ProfileDetails profile)
        {
            await _context.ProfileDetails.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProfileDetails profile)
        {
            _context.ProfileDetails.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int profileId)
        {
            var profile = await _context.ProfileDetails.FindAsync(profileId);
            if (profile != null)
            {
                _context.ProfileDetails.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }
    }
}

