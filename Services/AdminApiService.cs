using System.Net.Http.Json;
using ReadoraProject.Models;

namespace ReadoraProject.Services
{
    public class AdminApiService
    {
        private readonly HttpClient _http;
        public AdminApiService(HttpClient http) { _http = http; }

        // feedback 
        public async Task<List<FeedbackViewModel>> GetAllFeedbackAsync()
        {
            return await _http.GetFromJsonAsync<List<FeedbackViewModel>>("api/Admin/feedback")
                   ?? new List<FeedbackViewModel>();
        }

        public async Task MarkReadAsync(int id)
        {
            await _http.PutAsync($"api/Admin/mark-read/{id}", null);
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            await _http.DeleteAsync($"api/Admin/delete/{id}");
        }
        // QUERY OPERATIONS
        // ==========================================
        public async Task<List<QueryViewModel>> GetAllQueriesAsync()
        {
            return await _http.GetFromJsonAsync<List<QueryViewModel>>("api/Admin/queries")
                   ?? new List<QueryViewModel>();
        }

        public async Task ResolveQueryWithMsgAsync(int id, string message)
        {
            // Resolve request bhejna saath mein resolution message
            var requestData = new { message = message };
            await _http.PutAsJsonAsync($"api/Admin/resolve-query/{id}", requestData);
        }

        public async Task DeleteQueryAsync(int id)
        {
            await _http.DeleteAsync($"api/Admin/delete-query/{id}");
        }

        // CATEGORY OPERATIONS
        // ==========================================
        public async Task<List<CategoryDetails>> GetAllCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoryDetails>>("api/Admin/categories")
                   ?? new List<CategoryDetails>();
        }

        public async Task AddCategoryAsync(CategoryDetails category)
        {
            await _http.PostAsJsonAsync("api/Admin/add-category", category);
        }

        public async Task UpdateCategoryAsync(CategoryDetails category)
        {
            await _http.PutAsJsonAsync("api/Admin/update-category", category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _http.DeleteAsync($"api/Admin/delete-category/{id}");
        }
        // USER API CALLS
        public async Task<List<UserDetails>> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDetails>>("api/Admin/users")
                   ?? new List<UserDetails>();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Admin/delete-user/{id}");
            return response.IsSuccessStatusCode;
        }

        // --- CONTENT API CALLS ---

        public async Task<List<ContentViewModel>> GetAllContentsAsync()
        {
            return await _http.GetFromJsonAsync<List<ContentViewModel>>("api/Admin/contents")
                   ?? new List<ContentViewModel>();
        }

        public async Task<bool> UpdateStatusAsync(int id, bool status)
        {
            // PUT request for updating status (Approve/Reject)
            var response = await _http.PutAsync($"api/Admin/update-content-status/{id}/{status}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteContentAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Admin/delete-content/{id}");
            return response.IsSuccessStatusCode;
        }

        //Dashboard
        public async Task<DashboardViewModel> GetDashboardStatsAsync()
        {
            return await _http.GetFromJsonAsync<DashboardViewModel>("api/Admin/dashboard-stats");
        }
    }
}