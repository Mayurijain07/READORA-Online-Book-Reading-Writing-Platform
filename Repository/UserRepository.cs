using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly ReadoraDbContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(ReadoraDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public UserDetails? GetLoginDetails(string username, string password)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand(
                "SELECT UserId, Username, Password, RoleType FROM UserDetails WHERE Username = @username AND Password = @password",
                con);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();

            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new UserDetails
                {
                    UserId = Convert.ToInt32(dr["UserId"]),
                    Username = dr["Username"].ToString()!,
                    Password = dr["Password"].ToString()!,
                    RoleType = dr["RoleType"].ToString()!,
                };
            }

            return null;
        }




        public async Task<IEnumerable<UserDetails>> GetAll()
            => await _context.UserDetails.AsNoTracking().ToListAsync();

        public async Task<UserDetails?> GetUserByUsername(string username)
        {
            return await _context.UserDetails
               .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        }

        public async Task<bool> IsUsernameTaken(string username) 
        {
            
            return await _context.UserDetails.AnyAsync(u => u.Username == username);
        }
        public async Task RegisterUser(UserDetails user)
        {
            // 1. Database se sabse latest user ki ID nikalna (Auto-generation ke liye)
            var lastUser = await _context.UserDetails
                                         .OrderByDescending(u => u.UserId)
                                         .FirstOrDefaultAsync();

            // 2. Naya Registration Number calculate karna
            // Agar koi user nahi hai toh 1001 se start hoga
            int nextId = (lastUser != null) ? lastUser.UserId + 1 : 1001;
            user.RegistrationNumber = "MJ" + (1000 + nextId);

            // 3. Default values set karna
            user.IsActive = true;
            user.CreateDate = DateTime.Now;

            // 4. Database mein save karna
            _context.UserDetails.Add(user);
            await _context.SaveChangesAsync();
        }

        // Naya method: Admin authentication ke liye
        public AdminDetails? GetAdminDetails(string name, string password)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand(
                "SELECT AdminId, AdminName, AdminPassword FROM AdminDetails WHERE AdminName = @name AND AdminPassword = @password",
                con);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();

            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new AdminDetails
                {
                    AdminId = Convert.ToInt32(dr["AdminId"]),
                    AdminName = dr["AdminName"].ToString()!,
                    AdminPassword = dr["AdminPassword"].ToString()!
                };
            }

            return null;
        }
        //profile-----
        public async Task SwitchUserRoleAsync(int userId)
        {
            var user = await _context.UserDetails.FindAsync(userId);
            if (user != null)
            {
                user.RoleType = (user.RoleType == "Reader") ? "Writer" : "Reader";
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ProfileDetails?> GetProfileAsync(int userId, string token)
        {
            return await _context.ProfileDetails.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<UserDetails?> GetUserByIdAsync(int userId, string token)
        {
            return await _context.UserDetails.FindAsync(userId);
        }
    }

}





      