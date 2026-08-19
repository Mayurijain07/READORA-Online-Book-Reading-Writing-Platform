using ReadoraProject.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace ReadoraProject.Services
{
    public class UserApiService
    {
        private readonly HttpClient _http;
        public UserApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("MyApi");
        }
        // Register

        public async Task<(bool Success, string Message)> RegisterAsync(UserDetails user)
        {
            var response = await _http.PostAsJsonAsync("api/User/register", user);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return (true, "Registration Successful");

            return (false, body); 
        }

        //Login user
        public async Task<LoginResponse?> LoginAsync(string username, string password)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login",
                new { Username = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }

            return null;
        }
        // Get All
        public async Task<List<UserDetails>> GetAllAsync(string token)
        {
            _http.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _http.GetFromJsonAsync<List<UserDetails>>("api/User") ?? new List<UserDetails>();
        }
        // Admin login service 
        public async Task<AdminLoginResponse?> AdminLoginAsync(string adminName, string password)
        {
            // API ke "api/Auth/AdminLogin" endpoint ko call karein
            var response = await _http.PostAsJsonAsync("api/Auth/AdminLogin",
                new { AdminName = adminName, AdminPassword = password });

            if (response.IsSuccessStatusCode)
            {
                // AdminLoginResponse DTO ke saath response read karein
                return await response.Content.ReadFromJsonAsync<AdminLoginResponse>();
            }

            return null; // Login failed
        }
        //profile 
        public async Task<UserDetails?> GetUserByIdAsync(int userId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/{userId}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<UserDetails>();

            return null;
        }

        public async Task<ProfileDetails?> GetProfileAsync(int userId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Profile/{userId}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ProfileDetails>();

            return null;
        }

        public async Task SwitchUserRoleAsync(int userId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/User/switch-role/{userId}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            await _http.SendAsync(request);
        }

    }
}

