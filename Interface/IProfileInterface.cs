using ReadoraProject.Models;

namespace ReadoraProject.Interface
{
    public interface IProfileInterface
    {
        Task<ProfileDetails> GetByUserIdAsync(int userId);
        Task CreateAsync(ProfileDetails profile);
        Task UpdateAsync(ProfileDetails profile);
        Task DeleteAsync(int profileId);
    }
}
