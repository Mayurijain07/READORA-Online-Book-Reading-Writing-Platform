using Microsoft.EntityFrameworkCore;
using ReadoraProject.Models;

namespace ReadoraProject.Interface
{
    public interface IUserInterface
    {
       
        UserDetails? GetLoginDetails(string username, string password);
        Task<IEnumerable<UserDetails>> GetAll();
        Task<UserDetails?> GetUserByUsername(string username); 
        Task RegisterUser(UserDetails user);
        Task<bool> IsUsernameTaken(string username); 
        AdminDetails? GetAdminDetails(string name, string password);
        //profile----
        Task SwitchUserRoleAsync(int userId);
        Task<ProfileDetails?> GetProfileAsync(int userId, string token);
        Task<UserDetails?> GetUserByIdAsync(int userId, string token);

    }
}

