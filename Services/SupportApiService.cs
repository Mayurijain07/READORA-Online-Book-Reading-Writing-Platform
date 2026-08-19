using System.Net.Http.Json;
using ReadoraProject.Models;
using ReadoraProject.Interface; 

namespace ReadoraProject.Services
{
    public class SupportApiService : ISupportService
    {
        private readonly HttpClient _httpClient;

        public SupportApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddFeedbackAsync(FeedbackDetails feedback)
        {
            // API endpoint jahan data save hoga
            await _httpClient.PostAsJsonAsync("api/support/feedback", feedback);
        }

        public async Task AddQueryAsync(QueryDetails query)
        {
            await _httpClient.PostAsJsonAsync("api/support/query", query);
        }
        public async Task<IEnumerable<QueryDetails>> GetUserQueriesAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<QueryDetails>>($"api/support/user-queries/{userId}");
        }
    }
}