using Microsoft.EntityFrameworkCore;
using ReadoraProject.Models;
namespace ReadoraProject.Interface
{
    public interface IAdminService
    {
        // --- Feedback Methods ---
        Task<List<FeedbackViewModel>> GetFeedbackListAsync();
        Task<bool> UpdateFeedbackStatusAsync(int id, int adminId);
        Task<bool> DeleteFeedbackAsync(int id);
        // QUERY METHODS ---
        Task<List<QueryViewModel>> GetQueryListAsync();
        Task<bool> UpdateQueryStatusAsync(int id, int adminId, string message);
        Task<bool> DeleteQueryAsync(int id);
        // --- Category Methods ---
        Task<List<CategoryDetails>> GetCategoryListAsync();
        Task<bool> AddCategoryAsync(CategoryDetails category);
        Task<bool> UpdateCategoryAsync(CategoryDetails category);
        Task<bool> DeleteCategoryAsync(int id);

        // --- User Management Methods ---
        Task<List<UserDetails>> GetUserListAsync();
        Task<bool> DeleteUserAsync(int id);
        //Task<bool> MakeAdminAsync(int id); // Future proofing for Admin Profile

        // --- Content Management ---
        Task<List<ContentViewModel>> GetContentListAsync();
        Task<bool> UpdateContentStatusAsync(int id,bool status);
        Task<bool> DeleteContentAsync(int id);
        //---Dashbaordstats-----

        Task<DashboardViewModel> GetDashboardStatsAsync();
    }

}
