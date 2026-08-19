using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReadoraProject.Models;
using ReadoraProject.Services;
using ReadoraProject.Data; 
using Microsoft.EntityFrameworkCore;
using ReadoraProject.Repository;
using ReadoraProject.Interface;

namespace ReadoraProject.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly UserApiService _userApi;
        private readonly ReadoraDbContext _db;
        private readonly IInteractionInterface _interactionRepo;
        private readonly ContentApiService _service;

        public UserProfileModel(UserApiService userApi, ReadoraDbContext db, IInteractionInterface interactionRepo, ContentApiService service)

        {
            _userApi = userApi;
            _db = db;
            _interactionRepo = interactionRepo;
            _service = service;
        }

        // Properties section
        public UserDetails? AppUser { get; set; }
        public ProfileDetails? Profile { get; set; }
        public bool IsOwnProfile { get; set; }
        public bool IsFollowing { get; set; } 
        public int FollowerCount { get; set; } 
        public int FollowingCount { get; set; }
        public string Username => AppUser?.Username ?? "Guest";

        public string ProfileFullName => Profile?.FullName ?? "";
        public string RoleType => AppUser?.RoleType ?? "Reader";
        public string Bio => Profile?.Bio ?? "Add your bio!";
        public string PhotoUrl => Profile?.ProfilePhoto ?? "/images/default.jpg";

        public string CreateDate => AppUser?.CreateDate?.ToString("dd MMM yyyy") ?? "N/A";
        public string LastUpdated => Profile?.LastUpdated.ToString("dd MMM yyyy") ?? "N/A";

        public List<string> ReadingHistoryDetails { get; set; } = new List<string>();
        public List<string> FavoriteContentList { get; set; } = new List<string>();

        public List<ContentDetails> PublishedStories { get; set; } = new();
        // User ki reading history store karne ke liye
        public List<ContentDetails> ReadingHistory { get; set; } = new();
        // User ke favorite stories store karne ke liye
        public List<ContentDetails> FavoriteStories { get; set; } = new();
        // User ke unpublished work (drafts) store karne ke liye
        public List<ContentDetails> SavedDrafts { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(int? id) // 'id' parameter add kiya
        {
            // 1. Session se logged-in user (Aap) ki ID aur Token nikalen
            var loggedInUserId = HttpContext.Session.GetInt32("UserId");
            var token = HttpContext.Session.GetString("JWToken");

            if (loggedInUserId == null || string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            // 2. LOGIC: Agar URL mein 'id' hai (Belly), toh use fetch karo. 
            // Agar 'id' null hai, toh logged-in user (Deep) ki apni profile fetch karo.
            int targetUserId = id ?? loggedInUserId.Value;


            // 3. Check karein ki kya ye meri apni profile hai? 
            // (Iska use Frontend par 'Edit' button hide/show karne ke liye hoga)
            IsOwnProfile = (targetUserId == loggedInUserId.Value);

            // 4. API se target user (Belly ya Deep) ka data fetch karna
            AppUser = await _userApi.GetUserByIdAsync(targetUserId, token);

            if (AppUser != null)
            {
                Profile = await _userApi.GetProfileAsync(targetUserId, token);
                // 1. COUNTS FETCH KARNA (Repo use karke)
                FollowerCount = await _interactionRepo.GetFollowerCountAsync(targetUserId);
                FollowingCount = await _interactionRepo.GetFollowingCountAsync(targetUserId);
                IsFollowing = await _interactionRepo.IsFollowingAsync(loggedInUserId.Value, targetUserId);

                // 2. PUBLISHED STORIES FETCH KARNA (Database direct query)
                PublishedStories = await _db.ContentDetails
                    .Where(c => c.UserId == targetUserId && c.Status == true)
                    .OrderByDescending(c => c.ContentDate)
                    .ToListAsync();
                // A. READING HISTORY: User ne jo stories padhi hain (Top 5)
                ReadingHistory = await _db.ReadingHistoryDetails
                    .Where(rh => rh.UserId == targetUserId)
                    .Include(rh => rh.Content)
                    .Select(rh => rh.Content)
                    .Take(5).ToListAsync();

                // B. FAVORITES: Jo stories user ko pasand aayi hain
                FavoriteStories = await _db.FavouriteDetails
                    .Where(f => f.UserId == targetUserId)
                    .Include(f => f.Content)
                    .Select(f => f.Content)
                    .ToListAsync();

                // C. SAVED DRAFTS: Sirf apni profile par dikhega (Status = false matlab Draft)
                if (IsOwnProfile)
                {
                    SavedDrafts = await _db.ContentDetails
                        .Where(c => c.UserId == targetUserId && c.Status == false)
                        .OrderByDescending(c => c.ContentDate)
                        .ToListAsync();
                }
            }

            return Page();
        }
        //Switch logic
       public async Task<IActionResult> OnPostSwitchRoleAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var token = HttpContext.Session.GetString("JWToken");

            if (userId == null || string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            // 2. Role switch call mein token bhejein
            await _userApi.SwitchUserRoleAsync(userId.Value, token);

            // 3. Session mein naya role update karein taaki UI turant badle
            var updatedUser = await _userApi.GetUserByIdAsync(userId.Value, token);
            if (updatedUser != null)
            {
                HttpContext.Session.SetString("UserRole", updatedUser.RoleType ?? "Reader");
            }

            return RedirectToPage("/Dashboard");
        }

        // Follow/Unfollow button ke liye
        public async Task<IActionResult> OnPostToggleFollowAsync(int targetId)
        {
            var loggedInUserId = HttpContext.Session.GetInt32("UserId");
            if (loggedInUserId == null) return RedirectToPage("/Login");

            // 1. Check karo kya pehle se follow kar rahe ho?
            var existingFollow = await _db.FollowerDetails
                .FirstOrDefaultAsync(f => f.FollowerUserId == loggedInUserId.Value && f.FollowingUserId == targetId);

            if (existingFollow != null)
            {
                // Agar mil gaya, toh Unfollow kar do (Delete entry)
                _db.FollowerDetails.Remove(existingFollow);
            }
            else
            {
                // Agar nahi mila, toh Follow kar do (Add entry)
                _db.FollowerDetails.Add(new FollowerDetails
                {
                    FollowerUserId = loggedInUserId.Value,
                    FollowingUserId = targetId
                });
            }

            await _db.SaveChangesAsync();

            // Wapis usi profile page par bhej do (reload ho jayega aur counts update ho jayenge)
            return RedirectToPage(new { id = targetId });
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteContentAsync(id);

            // Wapas usi user ke profile par redirect karein
            int? userId = HttpContext.Session.GetInt32("UserId");
            return RedirectToPage("/UserProfile", new { id = userId });
        }

    }
}



